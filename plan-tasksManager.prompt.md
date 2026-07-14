## Plan: Build a Task Manager with ASP.NET Core MVC

TL;DR: Create a beginner-friendly ASP.NET Core MVC app with Identity authentication, EF Core, and SQLite. Start with a simple authenticated task list, then add categories, priorities, due dates, and a dashboard with basic statistics.

### Steps
1. Project setup
   - Create a new ASP.NET Core MVC project with SQLite support.
   - Add the necessary NuGet packages for EF Core, Identity, and SQL Lite.
   - Set up the solution structure so the app stays easy to follow.

2. Domain and data layer
   - Create models for users, tasks, categories, and priorities.
   - Add a database context and configure the relationships between tasks and lookup data.
   - Add initial EF Core migrations and verify the database is created locally.

3. Authentication and user ownership
   - Configure Identity so users can register and sign in.
   - Ensure each task belongs to the signed-in user so the app is secure and realistic.
   - Add basic access rules so only authenticated users can manage tasks.

4. Task CRUD features
   - Build create, read, update, and delete flows for tasks.
   - Add validation for required fields, due dates, and task titles.
   - Provide simple list and detail views for the user experience.

5. Categories, priorities, and filtering
   - Add category and priority selection when creating or editing tasks.
   - Allow filtering or sorting by category, priority, and due date.
   - Keep the UI simple so the concepts remain easy to learn.

6. Dashboard and statistics
   - Add a dashboard page showing counts such as total tasks, completed tasks, overdue tasks, and tasks due soon.
   - Display a small summary using simple charts or plain cards.
   - Keep the calculations easy to understand and debug.

7. Polish and validation
   - Improve the layout with Bootstrap styling.
   - Test the full flow from registration to task creation and dashboard review.
   - Refactor the code as needed to make the structure easier to follow.

### Relevant areas to implement
- Project scaffolding and startup configuration
- Authentication and user management
- Data models and EF Core configuration
- Controllers and views for tasks and dashboard
- Styling and basic validation

### Verification
1. Run the app locally and confirm the home page loads.
2. Register a user and verify login/logout works.
3. Create, edit, and delete tasks successfully.
4. Confirm tasks are stored in SQLite and appear correctly in the UI.
5. Review the dashboard statistics and ensure they reflect the current task data.

### Assumptions
- The first version will focus on a single authenticated user experience with task ownership.
- Categories and priorities will be simple dropdown-based values rather than a full admin system.
- The learning goal is clarity and progression, not production-level complexity.
