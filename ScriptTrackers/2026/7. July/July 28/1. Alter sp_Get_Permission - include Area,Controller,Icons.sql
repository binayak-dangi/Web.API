/*
    Fix: sp_Get_Permission was not populating Area, Controller, Action, PermissionCode,
    ParentOrder, ChildOrder, FontIcon, AreaIcon, IdParentPermission for the
    'GetAssignedPermissionList' and 'GetAllPermissionList' branches.

    Without these columns, GET /api/HRPermission/role/{id}/assigned returns rows with
    Controller = NULL, which makes it impossible for the frontend to know which left-menu
    items the role is actually allowed to see.

    This script re-creates the stored procedure with those columns populated.
*/
USE [WebAPIDB]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER Procedure [dbo].[sp_Get_Permission]
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
IdHRCompany bigint,
IdHREmployee bigint,
IdHRRole bigint,
IdHRPermission bigint,
CreateOnly bit,
ReadOnly bit,
EditOnly bit,
DeleteOnly bit,
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
IsDeleted bit,
IdGuid UniqueIdentifier,
Created_By nvarchar(100),
Updated_By nvarchar(100),
Created_On datetime,
Updated_On datetime
);
IF @paramFor = 'HRPermissionByRole'
Begin
    IF @paramType = 'GetAssignedPermissionList'
    Begin
        Insert Into @varTbl
        (
        Id,
        IdHRCompany,
        IdHRRole,
        IdHRPermission,
        CreateOnly,
        ReadOnly,
        EditOnly,
        DeleteOnly,
        IdParentPermission,
        Area,
        Controller,
        Action,
        PermissionCode,
        ParentOrder,
        ChildOrder,
        FontIcon,
        AreaIcon,
        IdGuid,
        IsActive,
        IsDeleted,
        Title,
        Description,
        Created_By,
        Updated_By,
        Created_On,
        Updated_On
        )
        select
         a.Id,
         a.IdHRCompany,
         b.IdHRRole,
         b.IdHRPermission,
         b.CreateOnly,
         b.ReadOnly,
         b.EditOnly,
         b.DeleteOnly,
         a.IdParentPermission,
         a.Area,
         a.Controller,
         a.Action,
         a.PermissionCode,
         a.ParentOrder,
         a.ChildOrder,
         a.FontIcon,
         a.AreaIcon,
         a.IdGUID,
         a.IsActive,
         a.IsDeleted,
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

    ELSE IF @paramType='GetAllPermissionList'
    Begin
      Insert Into @varTbl
      (
        Id,
        IdHRCompany,
        IdHRRole,
        IdHRPermission,
        CreateOnly,
        ReadOnly,
        EditOnly,
        DeleteOnly,
        IdParentPermission,
        Area,
        Controller,
        Action,
        PermissionCode,
        ParentOrder,
        ChildOrder,
        FontIcon,
        AreaIcon,
        IdGuid,
        IsActive,
        IsDeleted,
        Title,
        Description,
        Created_By,
        Updated_By,
        Created_On,
        Updated_On
        )
        select
         a.Id,
         a.IdHRCompany,
         0 IdHRRole,
         a.Id IdHRPermission,
         0,
         0,
         0,
         0,
         a.IdParentPermission,
         a.Area,
         a.Controller,
         a.Action,
         a.PermissionCode,
         a.ParentOrder,
         a.ChildOrder,
         a.FontIcon,
         a.AreaIcon,
         a.IdGUID,
         a.IsActive,
         a.IsDeleted,
         a.Title,
         a.Description,
         a.Created_By,
         a.Updated_By,
         a.Created_On,
         a.Updated_On
        from HRPermission a with(nolock)
        where a.Id not in(select x.IdHRPermission from HRRolePermissionLink x(nolock) where x.IdHRRole=@paramIdReference) and a.IsActive = 1 and a.IsDeleted=0
        UNION
        select
         a.Id,
         a.IdHRCompany,
         b.IdHRRole,
         b.IDHRPermission,
         ISNULL(b.CreateOnly,0),
         ISNULL(b.ReadOnly,0),
         ISNULL(b.EditOnly,0),
         ISNULL(b.DeleteOnly,0),
         a.IdParentPermission,
         a.Area,
         a.Controller,
         a.Action,
         a.PermissionCode,
         a.ParentOrder,
         a.ChildOrder,
         a.FontIcon,
         a.AreaIcon,
         a.IdGUID,
         a.IsActive,
         a.IsDeleted,
         a.Title,
         a.Description,
         a.Created_By,
         a.Updated_By,
         a.Created_On,
         a.Updated_On
        from HRPermission a with(nolock)
        Join HRRolePermissionLink b with(nolock) on a.Id = b.IDHRPermission
        where b.IdHRRole=@paramIdReference and a.IsActive = 1 and a.IsDeleted=0
    End

    ELSE IF(@paramType='UpdateMirrorTable')
    BEGIN
        truncate table HRRolePermissionLinkMirror;
    END

    ELSE IF @paramType='BulkUpdatePermissionList'
    Begin
        INSERT INTO [dbo].[HRRolePermissionLink]
           ([IdHRRole]
           ,[IdHRPermission]
           ,[CreateOnly]
           ,[ReadOnly]
           ,[EditOnly]
           ,[DeleteOnly]
           ,[IdHRCompany]
           )
        SELECT
             [IdHRRole]
            ,[IdHRPermission]
            ,[CreateOnly]
            ,[ReadOnly]
            ,[EditOnly]
            ,[DeleteOnly]
            ,[IdHRCompany]
          FROM [dbo].[HRRolePermissionLinkMirror] a(nolock)
          where a.IdHRPermission not in(select x.IdHRPermission from HRRolePermissionLink x(nolock) where x.IdHRRole=@paramIdReference);

          update a set CreateOnly=b.CreateOnly,ReadOnly=b.ReadOnly,DeleteOnly=b.DeleteOnly,EditOnly=b.EditOnly
          from [HRRolePermissionLink] a,HRRolePermissionLinkMirror b where a.IdHRPermission=b.IdHRPermission and a.IdHRRole=b.IdHRRole;

    End
  End

  IF @paramFor = 'HRPermissionByEmployee'
Begin
    IF @paramType = 'GetAssignedPermissionList'
    Begin
        Insert Into @varTbl
        (
        Id,
        IdHRCompany,
        IdHREmployee,
        IdHRPermission,
        CreateOnly,
        ReadOnly,
        EditOnly,
        DeleteOnly,
        IdParentPermission,
        Area,
        Controller,
        Action,
        PermissionCode,
        ParentOrder,
        ChildOrder,
        FontIcon,
        AreaIcon,
        IdGuid,
        IsActive,
        IsDeleted,
        Title,
        Description,
        Created_By,
        Updated_By,
        Created_On,
        Updated_On
        )
        select
         a.Id,
         a.IdHRCompany,
         b.IdHREmployee,
         b.IdHRPermission,
         b.CreateOnly,
         b.ReadOnly,
         b.EditOnly,
         b.DeleteOnly,
         a.IdParentPermission,
         a.Area,
         a.Controller,
         a.Action,
         a.PermissionCode,
         a.ParentOrder,
         a.ChildOrder,
         a.FontIcon,
         a.AreaIcon,
         a.IdGUID,
         a.IsActive,
         a.IsDeleted,
         a.Title,
         a.Description,
         a.Created_By,
         a.Updated_By,
         a.Created_On,
         a.Updated_On
        from HRPermission a with(nolock)
        Join HREmployeePermissionLink b with(nolock) on a.Id = b.IDHRPermission
        where b.IdHREmployee = @paramIdReference and a.IsActive = 1 and a.IsDeleted=0
    End

    ELSE IF @paramType='GetAllPermissionList'
    Begin
      Insert Into @varTbl
      (
        Id,
        IdHRCompany,
        IdHREmployee,
        IdHRPermission,
        CreateOnly,
        ReadOnly,
        EditOnly,
        DeleteOnly,
        IdParentPermission,
        Area,
        Controller,
        Action,
        PermissionCode,
        ParentOrder,
        ChildOrder,
        FontIcon,
        AreaIcon,
        IdGuid,
        IsActive,
        IsDeleted,
        Title,
        Description,
        Created_By,
        Updated_By,
        Created_On,
        Updated_On
        )
        select
         a.Id,
         a.IdHRCompany,
         0 IdHREmployee,
         a.Id IdHRPermission,
         0,
         0,
         0,
         0,
         a.IdParentPermission,
         a.Area,
         a.Controller,
         a.Action,
         a.PermissionCode,
         a.ParentOrder,
         a.ChildOrder,
         a.FontIcon,
         a.AreaIcon,
         a.IdGUID,
         a.IsActive,
         a.IsDeleted,
         a.Title,
         a.Description,
         a.Created_By,
         a.Updated_By,
         a.Created_On,
         a.Updated_On
        from HRPermission a with(nolock)
        where a.Id not in(select x.IdHRPermission from HREmployeePermissionLink x(nolock) where x.IdHREmployee=@paramIdReference) and a.IsActive = 1 and a.IsDeleted=0
        UNION
        select
         a.Id,
         a.IdHRCompany,
         b.IdHREmployee,
         b.IDHRPermission,
         ISNULL(b.CreateOnly,0),
         ISNULL(b.ReadOnly,0),
         ISNULL(b.EditOnly,0),
         ISNULL(b.DeleteOnly,0),
         a.IdParentPermission,
         a.Area,
         a.Controller,
         a.Action,
         a.PermissionCode,
         a.ParentOrder,
         a.ChildOrder,
         a.FontIcon,
         a.AreaIcon,
         a.IdGUID,
         a.IsActive,
         a.IsDeleted,
         a.Title,
         a.Description,
         a.Created_By,
         a.Updated_By,
         a.Created_On,
         a.Updated_On
        from HRPermission a with(nolock)
        Join HREmployeePermissionLink b with(nolock) on a.Id = b.IDHRPermission
        where b.IdHREmployee=@paramIdReference and a.IsActive = 1 and a.IsDeleted=0
    End

    ELSE IF(@paramType='UpdateMirrorTable')
    BEGIN
        truncate table HREmployeePermissionLinkMirror;
    END

    ELSE IF @paramType='BulkUpdatePermissionList'
    Begin
        INSERT INTO [dbo].[HREmployeePermissionLink]
           (IdHREmployee
           ,[IdHRPermission]
           ,[CreateOnly]
           ,[ReadOnly]
           ,[EditOnly]
           ,[DeleteOnly]
           ,[IdHRCompany]
           )
        SELECT
             IdHREmployee
            ,[IdHRPermission]
            ,[CreateOnly]
            ,[ReadOnly]
            ,[EditOnly]
            ,[DeleteOnly]
            ,[IdHRCompany]
          FROM [dbo].[HREmployeePermissionLinkMirror] a(nolock)
          where a.IdHRPermission not in(select x.IdHRPermission from HREmployeePermissionLink x(nolock) where x.IdHREmployee=@paramIdReference);

          update a set CreateOnly=b.CreateOnly,ReadOnly=b.ReadOnly,DeleteOnly=b.DeleteOnly,EditOnly=b.EditOnly
          from [HREmployeePermissionLink] a,HREmployeePermissionLinkMirror b where a.IdHRPermission=b.IdHRPermission and a.IdHREmployee=b.IdHREmployee;

    End
  End

   IF @paramFor='GetPermissionMenuLst'
   Begin
   Insert into @varTbl
   (
        Id,
        IdHRCompany,
        IdHRPermission,
        CreateOnly,
        ReadOnly,
        EditOnly,
        DeleteOnly,
        IdParentPermission,
        Area,
        Controller,
        Action,
        PermissionCode,
        ParentOrder,
        ChildOrder,
        FontIcon,
        AreaIcon,
        IdGuid,
        IsActive,
        IsDeleted,
        Title,
        Description
   )
   select
        a.Id,
        a.IdHRCompany,
        b.IdHRPermission,
        b.CreateOnly,
        b.ReadOnly,
        b.EditOnly,
        b.DeleteOnly,
        a.IdParentPermission,
        a.Area,
        a.Controller,
        a.Action,
        a.PermissionCode,
        a.ParentOrder,
        a.ChildOrder,
        a.FontIcon,
        a.AreaIcon,
        a.IdGuid,
        a.IsActive,
        a.IsDeleted,
        a.Title,
        a.Description
        From HRPermission a with(nolock) 
        Join HREmployeePermissionLink b with(nolock) on a.Id=b.IdHRPermission
        where b.IdHREmployee=@paramIdReference and a.IsActive=1 and IsDeleted=0 
        Union
   select
        a.Id,
        a.IdHRCompany,
        b.IdHRPermission,
        b.CreateOnly,
        b.ReadOnly,
        b.EditOnly,
        b.DeleteOnly,
        a.IdParentPermission,
        a.Area,
        a.Controller,
        a.Action,
        a.PermissionCode,
        a.ParentOrder,
        a.ChildOrder,
        a.FontIcon,
        a.AreaIcon,
        a.IdGuid,
        a.IsActive,
        a.IsDeleted,
        a.Title,
        a.Description
        From HRPermission a with(nolock) 
        Join HRRolePermissionLink b with(nolock) on a.Id=b.IdHRPermission
        Join HREmployee c with(nolock) on b.IdHRRole =c.IdHRRole and c.Id=@paramIdReference
        where a.IsActive=1  and a.IsDeleted=0 and a.Id not in(select x.IdHRPermission from HREmployeePermissionLink x where x.IdHREmployee=@paramIdReference)
   End
  select
  Id,
  IdHRCompany,
  IdHREmployee,
  IdHRRole,
  IdHRPermission,
  CreateOnly,
  ReadOnly,
  EditOnly,
  DeleteOnly,
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
  IdGuid,
  IsDeleted,
  Created_By ,
  Updated_By,
  Created_On ,
  Updated_On
  from @varTbl
  End
