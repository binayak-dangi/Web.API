/*
    Seeds all six Setup-area HRPermission rows from scratch, using
    lucide-react export names for FontIcon/AreaIcon.

    Replaces the original July 17 seed (which only inserted "Employee" with
    Bootstrap Icon classes) with a single consistent insert covering every
    Setup controller: HRCompany, HRBranch, HRRole, HRCorporateTitle,
    HRFunctionalTitle, HREmployee. Without all six existing, the
    dynamically-built sidebar/top menu can't show the corresponding items.

    Safe to re-run: the existing Setup-area rows (and any role/employee
    links pointing at them) are removed first so this doesn't create
    duplicates.

    ParentOrder groups everything under Area='Setup' together; ChildOrder
    controls the order items appear within the Setup submenu.

    IMPORTANT: after running this, the new permission Ids are unassigned —
    go to the Security page and grant them to the relevant roles/employees,
    or they'll sit in the catalogue but stay invisible in the menu.
*/
USE [WebAPIDB]
GO



-- Fresh insert, all six Setup controllers
INSERT INTO HRPermission
(
    IdParentPermission, Title, Description, Area, Controller, Action, PermissionCode,
    ParentOrder, ChildOrder, FontIcon, AreaIcon, IsActive, Created_By, Updated_By,
    Created_On, Updated_On, IdGUID, IsDeleted, IdHRCompany
)
VALUES
(0, 'Companies', 'Registered legal entities', 'Setup', 'HRCompany', 'GetCompanies', 'SETUP_COMPANY_VIEW',
 1, 1, 'Building2', 'Settings2', 1, 'System', 'System', GETDATE(), GETDATE(), NEWID(), 0, 1),

(0, 'Branches', 'Office & site locations', 'Setup', 'HRBranch', 'GetBranches', 'SETUP_BRANCH_VIEW',
 1, 2, 'GitBranch', 'Settings2', 1, 'System', 'System', GETDATE(), GETDATE(), NEWID(), 0, 1),

(0, 'Roles', 'Access roles for employees', 'Setup', 'HRRole', 'GetRoles', 'SETUP_ROLE_VIEW',
 1, 3, 'ShieldCheck', 'Settings2', 1, 'System', 'System', GETDATE(), GETDATE(), NEWID(), 0, 1),

(0, 'Corporate Titles', 'Salary grades & levels', 'Setup', 'HRCorporateTitle', 'GetCorporateTitles', 'SETUP_CORPTITLE_VIEW',
 1, 4, 'IdCard', 'Settings2', 1, 'System', 'System', GETDATE(), GETDATE(), NEWID(), 0, 1),

(0, 'Functional Titles', 'Position headers & codes', 'Setup', 'HRFunctionalTitle', 'GetFunctionalTitles', 'SETUP_FUNCTITLE_VIEW',
 1, 5, 'Tags', 'Settings2', 1, 'System', 'System', GETDATE(), GETDATE(), NEWID(), 0, 1),

(0, 'Employee', 'Employee Management', 'Setup', 'HREmployee', 'GetEmployees', 'SETUP_EMPLOYEE_VIEW',
 1, 6, 'Users', 'Settings2', 1, 'System', 'System', GETDATE(), GETDATE(), NEWID(), 0, 1);
GO
