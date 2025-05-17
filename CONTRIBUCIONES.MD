# Guía de Contribuciones

Este documento describe el **flujo de trabajo** y las **buenas prácticas** para que cada uno de los cuatro contribuidores (fronend y backend) colabore de forma ordenada en el proyecto.

---

## 1. Estructura de Ramas

* `main` (o `master`): rama estable con el código de producción.
* `develop`: rama de integración donde se mezclan las contribuciones antes de una nueva versión.
* Ramas de características (feature branches): cada contribuidor crea su propia rama a partir de `develop`:

  * **Frontend**:

    * `feature/frontend-<nombre_contribuidor>`
  * **Backend**:

    * `feature/backend-<nombre_contribuidor>`

Ejemplo:

```bash
git checkout develop
git pull origin develop
git checkout -b feature/frontend-alex
```

---

## 2. Flujo de Trabajo

1. **Sincronizar** tu rama local con `develop` antes de empezar:

   ```bash
   git checkout develop
   git pull origin develop
   ```
2. **Crear** tu rama de feature:

   ```bash
   git checkout -b feature/backend-maria
   ```
3. **Desarrollar** tu tarea (frontend o backend) en esa rama.
4. **Hacer commits** descriptivos y atómicos:

   ```bash
   git add .
   git commit -m "feat: agregar endpoint de login en AuthController"
   ```
5. **Subir** tu rama al repositorio remoto:

   ```bash
   git push origin feature/backend-maria
   ```
6. **Abrir** un Pull Request (PR) desde GitHub:

   * Base: `develop`
   * Comparación: `feature/backend-maria`

---

## 3. Convenciones de Commit

* Usa prefijos convencionales:

  * `feat:` nueva funcionalidad
  * `fix:` corrección de errores
  * `docs:` cambios en documentación
  * `refactor:` refactorización sin cambio de funcionalidad
  * `test:` agregar o corregir pruebas
  * `chore:` tareas de mantenimiento

Ejemplo:

```
feat(frontend): implementar componente de lista de inmuebles
```

---

## 4. Revisión de Pull Requests

Al crear un PR, asegúrate de:

* Describir claramente los **cambios** y **motivación**.
* Referenciar **issues** o **tareas** relacionadas.
* Incluir **capturas de pantalla** o **ejemplos** si aplica.
* Etiquetar al **revisor** asignado (otro miembro del equipo).

Los revisores deben:

* Verificar que los **tests** pasen correctamente.
* Probar manualmente las funcionalidades en entorno local.
* Sugerir mejoras de **estilo** o **arquitectura**.

---

## 5. Fusión de Ramas

1. Una vez aprobado el PR, realizar el **merge** en `develop` mediante el botón de GitHub.
2. **Eliminar** la rama de feature remota y local:

   ```bash
   git push origin --delete feature/backend-maria
   git branch -d feature/backend-maria
   ```
3. Periodicamente, se hará un **release** de `develop` a `main` con una etiqueta semántica (`v1.0.0`, etc.).

---
