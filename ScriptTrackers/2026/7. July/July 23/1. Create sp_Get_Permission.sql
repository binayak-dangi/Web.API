Create Procedure sp_Get_Permission
(
@paramFor VARCHAR(50),
@paramType VARCHAR(50),
@paramIdReference bigint
)
As
Begin
Declare @varTbl Table
(
Id bigint,
IdHREmployee bigint,
IdHRRole bigint,
IdHRPermission bigint,
CreateOnly bit,
ReadOnly bit,
EditOnly bit,
DeleteOnly bit,
ClientIp varchar(100),
IdParentPermission bigint,
Title varchar(100),
Description nvarchar(100),
Area nvarchar(100),
Controller varchar(100),
Action nvarchar(100),
PermissionCode nvarchar(100),
ParentOrder bigint,
ChildOrder bigint,
FontIcon nvarchar(100),
AreaIcon nvarchar(100),
IsActive bit,
Created_By nvarchar(100),
Updated_By nvarchar(100),
Created_On datetime,
Updated_On datetime
)
IF @paramFor = 'HRPermissionByRole'
Begin
    IF @paramType = 'GetPermissionList'
    Begin
        Insert Into @varTbl
        (
        Id,
        IdHRRole,
        IdHRPermission,
        CreateOnly,
        ReadOnly,
        EditOnly,
        DeleteOnly,
        Title,
        Description,
        Created_By,
        Updated_By,
        Created_On,
        Updated_On
        )
        select
         a.Id,
        b.IdHRRole,
        b.IDHRPermission,
        b.CreateOnly,
        b.ReadOnly,
        b.EditOnly,
        b.DeleteOnly,
        a.Title,
        a.Description,
        a.Created_By,
        a.Updated_By,
        a.Created_On,
        a.Updated_On
        from HRPermission a with(nolock)
        Join HRRolePermissionLink b with(nolock) on a.Id = b.IDHRPermission
        where b.IdHRRole = @paramIdReference and a.IsActive = 1 and a.IsDeleted=0
    End
End
select 
Id,
IdHREmployee,
IdHRRole,
IdHRPermission,
CreateOnly,
ReadOnly,
EditOnly,
DeleteOnly,
ClientIp,
IdParentPermission,
Title ,
Description ,
Area ,
Controller ,
Action ,
PermissionCode ,
ParentOrder ,
ChildOrder ,
FontIcon ,
AreaIcon,
IsActive ,
Created_By ,
Updated_By,
Created_On ,
Updated_On 
from @varTbl
End