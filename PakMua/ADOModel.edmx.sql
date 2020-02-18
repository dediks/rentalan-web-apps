
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 12/27/2019 04:26:09
-- Generated from EDMX file: C:\Users\Dediksu\documents\visual studio 2015\Projects\PakMua\PakMua\ADOModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [db_rentalan];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_tb_order_tb_member]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tb_order] DROP CONSTRAINT [FK_tb_order_tb_member];
GO
IF OBJECT_ID(N'[dbo].[FK_tb_order_tb_mobil]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tb_order] DROP CONSTRAINT [FK_tb_order_tb_mobil];
GO
IF OBJECT_ID(N'[dbo].[FK_tb_payment_tb_order]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tb_payment] DROP CONSTRAINT [FK_tb_payment_tb_order];
GO
IF OBJECT_ID(N'[dbo].[FK_tb_pengembalian_tb_order]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tb_pengembalian] DROP CONSTRAINT [FK_tb_pengembalian_tb_order];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[sysdiagrams]', 'U') IS NOT NULL
    DROP TABLE [dbo].[sysdiagrams];
GO
IF OBJECT_ID(N'[dbo].[tb_member]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tb_member];
GO
IF OBJECT_ID(N'[dbo].[tb_mobil]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tb_mobil];
GO
IF OBJECT_ID(N'[dbo].[tb_order]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tb_order];
GO
IF OBJECT_ID(N'[dbo].[tb_payment]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tb_payment];
GO
IF OBJECT_ID(N'[dbo].[tb_pengembalian]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tb_pengembalian];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'sysdiagrams'
CREATE TABLE [dbo].[sysdiagrams] (
    [name] nvarchar(128)  NOT NULL,
    [principal_id] int  NOT NULL,
    [diagram_id] int IDENTITY(1,1) NOT NULL,
    [version] int  NULL,
    [definition] varbinary(max)  NULL
);
GO

-- Creating table 'tb_member'
CREATE TABLE [dbo].[tb_member] (
    [id_member] int  NOT NULL,
    [nama] varchar(50)  NULL,
    [alamat] varchar(max)  NULL,
    [no_hp] varchar(50)  NULL
);
GO

-- Creating table 'tb_mobil'
CREATE TABLE [dbo].[tb_mobil] (
    [id_mobil] int  NOT NULL,
    [nama] varchar(50)  NULL,
    [merk] varchar(50)  NULL,
    [model] varchar(50)  NULL,
    [varian] varchar(50)  NULL,
    [mesin] int  NULL,
    [tenaga] int  NULL,
    [tempat_duduk] int  NULL,
    [transmisi] char(10)  NULL,
    [ac] char(10)  NULL,
    [harga_sewa] decimal(19,4)  NULL,
    [deskripsi] varchar(max)  NULL,
    [foto] varchar(255)  NULL
);
GO

-- Creating table 'tb_order'
CREATE TABLE [dbo].[tb_order] (
    [id_order] int  NOT NULL,
    [no_invoice] varchar(50)  NULL,
    [tgl_order] datetime  NULL,
    [ambil] datetime  NULL,
    [kembali] datetime  NULL,
    [jumlah_hari] int  NULL,
    [jumlah_mobil] int  NULL,
    [total_harga] decimal(19,4)  NULL,
    [id_member] int  NULL,
    [id_mobil] int  NULL,
    [status] varchar(50)  NULL
);
GO

-- Creating table 'tb_payment'
CREATE TABLE [dbo].[tb_payment] (
    [id_payment] int  NOT NULL,
    [nama] varchar(50)  NULL,
    [nama_bank] varchar(50)  NULL,
    [no_rekening] varchar(50)  NULL,
    [transfer] decimal(19,4)  NULL,
    [id_order] int  NULL
);
GO

-- Creating table 'tb_pengembalian'
CREATE TABLE [dbo].[tb_pengembalian] (
    [id_pengembalian] int  NOT NULL,
    [id_order] int  NULL,
    [tgl_pengembalian] datetime  NULL,
    [denda] decimal(19,4)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [diagram_id] in table 'sysdiagrams'
ALTER TABLE [dbo].[sysdiagrams]
ADD CONSTRAINT [PK_sysdiagrams]
    PRIMARY KEY CLUSTERED ([diagram_id] ASC);
GO

-- Creating primary key on [id_member] in table 'tb_member'
ALTER TABLE [dbo].[tb_member]
ADD CONSTRAINT [PK_tb_member]
    PRIMARY KEY CLUSTERED ([id_member] ASC);
GO

-- Creating primary key on [id_mobil] in table 'tb_mobil'
ALTER TABLE [dbo].[tb_mobil]
ADD CONSTRAINT [PK_tb_mobil]
    PRIMARY KEY CLUSTERED ([id_mobil] ASC);
GO

-- Creating primary key on [id_order] in table 'tb_order'
ALTER TABLE [dbo].[tb_order]
ADD CONSTRAINT [PK_tb_order]
    PRIMARY KEY CLUSTERED ([id_order] ASC);
GO

-- Creating primary key on [id_payment] in table 'tb_payment'
ALTER TABLE [dbo].[tb_payment]
ADD CONSTRAINT [PK_tb_payment]
    PRIMARY KEY CLUSTERED ([id_payment] ASC);
GO

-- Creating primary key on [id_pengembalian] in table 'tb_pengembalian'
ALTER TABLE [dbo].[tb_pengembalian]
ADD CONSTRAINT [PK_tb_pengembalian]
    PRIMARY KEY CLUSTERED ([id_pengembalian] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [id_member] in table 'tb_order'
ALTER TABLE [dbo].[tb_order]
ADD CONSTRAINT [FK_tb_order_tb_member]
    FOREIGN KEY ([id_member])
    REFERENCES [dbo].[tb_member]
        ([id_member])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_tb_order_tb_member'
CREATE INDEX [IX_FK_tb_order_tb_member]
ON [dbo].[tb_order]
    ([id_member]);
GO

-- Creating foreign key on [id_mobil] in table 'tb_order'
ALTER TABLE [dbo].[tb_order]
ADD CONSTRAINT [FK_tb_order_tb_mobil]
    FOREIGN KEY ([id_mobil])
    REFERENCES [dbo].[tb_mobil]
        ([id_mobil])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_tb_order_tb_mobil'
CREATE INDEX [IX_FK_tb_order_tb_mobil]
ON [dbo].[tb_order]
    ([id_mobil]);
GO

-- Creating foreign key on [id_order] in table 'tb_payment'
ALTER TABLE [dbo].[tb_payment]
ADD CONSTRAINT [FK_tb_payment_tb_order]
    FOREIGN KEY ([id_order])
    REFERENCES [dbo].[tb_order]
        ([id_order])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_tb_payment_tb_order'
CREATE INDEX [IX_FK_tb_payment_tb_order]
ON [dbo].[tb_payment]
    ([id_order]);
GO

-- Creating foreign key on [id_order] in table 'tb_pengembalian'
ALTER TABLE [dbo].[tb_pengembalian]
ADD CONSTRAINT [FK_tb_pengembalian_tb_order]
    FOREIGN KEY ([id_order])
    REFERENCES [dbo].[tb_order]
        ([id_order])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_tb_pengembalian_tb_order'
CREATE INDEX [IX_FK_tb_pengembalian_tb_order]
ON [dbo].[tb_pengembalian]
    ([id_order]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------