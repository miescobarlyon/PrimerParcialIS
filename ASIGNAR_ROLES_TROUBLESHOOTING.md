# ASIGNAR ROLES - TROUBLESHOOTING GUIDE

## Common Issues and Solutions

### Issue 1: No roles showing in "Perfiles disponibles" list

**Symptoms:**
- User is selected but available profiles list is empty
- Already-assigned profiles show correctly on the right

**Possible Causes & Solutions:**

1. **No roles exist in database**
   - Solution: Create at least one Perfil in the "Roles" administration menu first
   - Then refresh the AsignarRoles form

2. **All roles already assigned to this user**
   - Solution: This is correct behavior - the list filters out already-assigned profiles
   - Remove some profiles to see them reappear in available list
   - Or select a different user with fewer assignments

3. **Database connection issue**
   - Check: Is the database online and accessible?
   - Check: Connection string in app.config
   - Solution: Verify connection in other forms (AdministrarRoles works?)

4. **PerfilMapper.Listar() returning empty**
   - Check: Run "ListarPerfiles" SP directly in SQL Server
   - Verify: It returns profile records
   - Check: NOMBRE and PERFIL_ID fields exist and have data

---

### Issue 2: Roles list not updating after assignment

**Symptoms:**
- Profile assigned successfully (success message shows)
- But available list doesn't update immediately
- Need to reselect user to see changes

**Solution:**
- This should not happen with current code
- If it does, ensure all THREE refresh calls are in buttonAsignar_Click():
  1. `CargarPerfilesDeUsuario()` - refresh assigned list
  2. `CargarPerfilesDisponibles()` - refresh available list  
  3. `CargarDetalles()` - update details

---

### Issue 3: Can assign same profile twice to same user

**Symptoms:**
- Assign profile to user
- Profile appears in both available and assigned lists
- Can click "Asignar" again

**Cause:** Duplicate check not working

**Solution:**
1. Check `UsuarioPerfilService.AsignarPerfil()` has this code:
```csharp
if (perfilesActuales.Any(p => p.Id == perfil.Id))
{
    OnEnviarError("Este perfil ya está asignado a este usuario.");
    return false;
}
```

2. Verify `ListarPerfilesDeUsuario()` correctly returns profiles:
   - Check SP "ObtenerPerfilesDelUsuario" in SQL
   - Verify it returns PERFIL_ID and NOMBRE columns

3. Verify `CargarPerfilesDisponibles()` filters correctly:
```csharp
List<BE.Perfil> perfilesDisponibles = todosLosPerfiles
    .Where(p => !perfilesDelUsuario.Any(pu => pu.Id == p.Id))
    .ToList();
```

---

### Issue 4: "Este perfil ya está asignado al usuario" when shouldn't be

**Symptoms:**
- Try to assign profile that shouldn't be assigned yet
- Get error message about already assigned

**Cause:** Filtering or comparison issue

**Solution:**
1. Verify profile IDs are matching correctly:
   - Add Debug.WriteLine() calls to print profile IDs
   - Check if type conversion (int) is causing issues

2. Verify comparison uses .Id consistently:
   - CORRECT: `pu.Id == p.Id`
   - WRONG: `pu == p` (object reference comparison)

3. Check database has unique profile records

---

### Issue 5: Multiple users can't have the same role

**Symptoms:**
- Assign "Administrador" to User A - success
- Try to assign "Administrador" to User B
- Get error or profile doesn't save

**Cause:** Business logic incorrectly preventing this

**Note:** This is ALLOWED by design. If it's not working:

**Solution:**
1. Check table structure:
```sql
SELECT * FROM USUARIO_PERFIL
-- Should allow multiple rows with same PERFIL_ID but different USUARIO_ID
```

2. Verify SP "AsignarPerfilAUsuario":
```sql
-- Should not check if perfil exists elsewhere
-- Only check if THIS user already has THIS perfil
```

3. Verify BLL doesn't have global duplicate check:
```csharp
// WRONG - prevents all users from having same profile:
if (BLL.allAssignments.Any(a => a.PerfilId == perfil.Id))
    return false;

// CORRECT - only prevents same user same profile:
if (perfilesActuales.Any(p => p.Id == perfil.Id))
    return false;
```

---

### Issue 6: Performance slow when loading roles

**Symptoms:**
- Form takes long time to load
- Selection changes are slow
- "Perfiles disponibles" takes time to populate

**Cause:** Inefficient queries

**Solution:**
1. Check `CargarPerfilesDisponibles()` is not calling DB multiple times
2. Verify LINQ filtering is client-side, not DB-side:
   ```csharp
   // Gets all from DB first (1 call), then filters in memory (fast)
   List<BE.Perfil> todosLosPerfiles = _service.ListarPerfiles();
   List<BE.Perfil> perfilesDelUsuario = _service.ListarPerfilesDeUsuario(usuario);
   List<BE.Perfil> perfilesDisponibles = todosLosPerfiles
       .Where(p => !perfilesDelUsuario.Any(pu => pu.Id == p.Id))
       .ToList();
   ```

3. If many profiles (100+), consider caching or pagination

---

### Issue 7: Form not responding to user selection

**Symptoms:**
- Click on user in listBoxUsuarios
- Nothing happens
- Buttons don't enable/disable

**Cause:** Event handler not wired or listBox not configured

**Solution:**
1. In Designer, verify `listBoxUsuarios`:
   - Has SelectionMode = One
   - Has SelectedValueChanged event = listBoxUsuarios_SelectedValueChanged

2. In Form_Load verify:
   - `buttonAsignar.Enabled = false` (set in constructor)
   - `buttonRemover.Enabled = false` (set in constructor)

3. Check event handler exists and has correct signature:
```csharp
private void listBoxUsuarios_SelectedValueChanged(object sender, EventArgs e)
{
    bool hayUsuario = listBoxUsuarios.SelectedItem != null;
    buttonAsignar.Enabled = hayUsuario;
    buttonRemover.Enabled = hayUsuario;
    // ... rest of code
}
```

---

## Debug Checklist

When troubleshooting, verify in this order:

- [ ] Database connection is active
- [ ] USUARIO, PERFIL, and USUARIO_PERFIL tables have data
- [ ] SPs work correctly when run directly in SQL
- [ ] PerfilMapper.Listar() returns profiles
- [ ] UsuarioService.Listar() returns users
- [ ] listBoxUsuarios displays users with DisplayMember="User"
- [ ] Selecting a user triggers SelectedValueChanged event
- [ ] CargarPerfilesDisponibles() executes and filters correctly
- [ ] listBoxPerfilesDisponibles displays with DisplayMember="Nombre"
- [ ] Assignment/removal operations call all three Cargar* methods

---

## Quick Test Script

```csharp
// In AsignarRoles constructor or Load, add temporarily:
private void TestData()
{
    var users = _service.ListarUsuarios();
    Debug.WriteLine($"Users found: {users.Count}");
    
    var profiles = _service.ListarPerfiles();
    Debug.WriteLine($"Profiles found: {profiles.Count}");
    
    if (users.Count > 0)
    {
        var userProfiles = _service.ListarPerfilesDeUsuario(users[0]);
        Debug.WriteLine($"User {users[0].User} has {userProfiles.Count} profiles");
    }
}
```

Call this in Form_Load: `TestData();`
Check Debug output for actual data counts.

---

## Support Resources

- See: ASIGNAR_ROLES_FIXES.md
- See: COMPLETE_IMPLEMENTATION_SUMMARY.md
- See: QUICK_REFERENCE.md
- Check: SQL\ConfigurarRolesYPermisos.sql for schema
