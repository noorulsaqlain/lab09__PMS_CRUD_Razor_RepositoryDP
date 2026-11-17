-- ===================================================
-- Database Creation Script for PMS (Product Management System)
-- ===================================================

-- Create database if it does not already exist
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'pmsdb')
BEGIN
    CREATE DATABASE [pmsdb];
END
GO

USE [pmsdb];
GO

-- Create products table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'products' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
    CREATE TABLE [dbo].[products] (
        [Id]          INT           IDENTITY (1, 1) NOT NULL,
        [Title]       VARCHAR (50)  NOT NULL,
        [description] VARCHAR (50)  NOT NULL,
        [image]       VARCHAR (255) NOT NULL,
        [price]       INT           NOT NULL,
        [quantity]    INT           NOT NULL,
        [status]      VARCHAR (150) NOT NULL,
        PRIMARY KEY CLUSTERED ([Id] ASC)
    );
END
GO

-- Sample Seed Data (Inserted if table is empty)
IF NOT EXISTS (SELECT 1 FROM [dbo].[products])
BEGIN
    INSERT INTO [dbo].[products] ([Title], [description], [image], [price], [quantity], [status])
    VALUES 
        ('Wireless Mouse', 'Ergonomic optical wireless mouse', 'https://images.unsplash.com/photo-1527864550417-7fd91fc51a46?w=200', 25, 50, 'Active'),
        ('Mechanical Keyboard', 'RGB Backlit Mechanical Keyboard', 'https://images.unsplash.com/photo-1587829741301-dc798b83add3?w=200', 75, 20, 'Active'),
        ('Gaming Headset', 'Noise-cancelling over-ear headset', 'https://images.unsplash.com/photo-1505740420928-5e560c06d30e?w=200', 50, 0, 'Out of Stock');
END
GO
