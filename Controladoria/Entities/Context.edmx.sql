
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 10/23/2013 12:00:51
-- Generated from EDMX file: C:\Users\bortiz\Desktop\Arquivos\Projetos_VS\ProtocoloWeb\v3\Controladoria\Entities\Context.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [HomoProtocoloWeb];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Formulario]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Formulario];
GO
IF OBJECT_ID(N'[dbo].[Usuarios]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Usuarios];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Usuarios'
CREATE TABLE [dbo].[Usuarios] (
    [idUsuario] int IDENTITY(1,1) NOT NULL,
    [Login] varchar(50)  NULL,
    [Formulario] bit  NULL,
    [Pesquisar] bit  NULL,
    [Controladoria] bit  NULL,
    [Financeira] bit  NULL,
    [Comex] bit  NULL,
    [PCeAdiantamentos] bit  NULL,
    [Fiscal] bit  NULL,
    [Fornecedor] bit  NULL,
    [Input] bit  NULL,
    [Administrador] bit  NULL,
    [Exclusao] bit  NULL
);
GO

-- Creating table 'Formulario'
CREATE TABLE [dbo].[Formulario] (
    [idFormulario] int IDENTITY(1,1) NOT NULL,
    [Empresa] varchar(100)  NULL,
    [CNPJ] varchar(100)  NULL,
    [Matriz] varchar(50)  NULL,
    [NumNF] varchar(50)  NULL,
    [NumPedido] varchar(50)  NULL,
    [NumAP] varchar(50)  NULL,
    [dtVencimento] datetime  NULL,
    [valorNF] varchar(20)  NULL,
    [dtEmissaoDOC] datetime  NULL,
    [codJustificativa] varchar(255)  NULL,
    [Departamento] varchar(100)  NULL,
    [tipoDocumento] varchar(50)  NULL,
    [documento] varchar(200)  NULL,
    [statusControladoria] bit  NULL,
    [statusFinanceira] bit  NULL,
    [statusComex] bit  NULL,
    [statusPCeAdiantamentos] bit  NULL,
    [statusFiscal] bit  NULL,
    [statusFornecedor] bit  NULL,
    [statusInput] bit  NULL,
    [dataCadastro] datetime  NULL,
    [Justificativa] nvarchar(max)  NULL,
    [concluido] bit  NULL,
    [loginSolicitante] varchar(50)  NULL,
    [emailSolicitante] varchar(50)  NULL,
    [vinculo] varchar(50)  NULL,
    [tipoContrato] varchar(50)  NULL,
    [contratoInicio] datetime  NULL,
    [contratoFim] datetime  NULL,
    [receitaFederal] datetime  NULL,
    [prefeitura] datetime  NULL,
    [cnae] datetime  NULL,
    [optanteSimples] bit  NULL,
    [optanteSimplesData] datetime  NULL,
    [sintegra] datetime  NULL,
    [juntaComercial] datetime  NULL,
    [IRpct] decimal(18,2)  NULL,
    [IRvalor] varchar(20)  NULL,
    [PCCpct] decimal(18,2)  NULL,
    [PCCvalor] varchar(20)  NULL,
    [ISSpct] decimal(18,2)  NULL,
    [ISSvalor] varchar(20)  NULL,
    [INSSpct] decimal(18,2)  NULL,
    [INSSvalor] varchar(20)  NULL,
    [liquido] varchar(20)  NULL,
    [cadastroMunicipal] datetime  NULL,
    [CodRetencaoISS] varchar(50)  NULL,
    [PCCnfs] varchar(50)  NULL,
    [CFOP] varchar(50)  NULL,
    [ZA] bit  NULL,
    [ZS] bit  NULL,
    [NT] bit  NULL,
    [codServico] varchar(50)  NULL,
    [valorDeducao] varchar(50)  NULL,
    [aliquota] varchar(50)  NULL,
    [tipoDeDocumento] varchar(50)  NULL,
    [tributacaoServico] varchar(50)  NULL,
    [codigoPrestador] varchar(50)  NULL,
    [atribuicaoISS] varchar(50)  NULL,
    [situacaoNTFS] varchar(50)  NULL,
    [ISSretido] varchar(50)  NULL,
    [registroTributacao] varchar(50)  NULL,
    [optSimples] varchar(50)  NULL,
    [tipoNFConv] varchar(50)  NULL,
    [codServicoMun] varchar(50)  NULL,
    [tpTribServico] varchar(50)  NULL,
    [lote] varchar(50)  NULL,
    [Reprovado] bit  NULL,
    [Excluido] bit  NULL,
    [IRbase] varchar(20)  NULL,
    [PCCbase] varchar(20)  NULL,
    [ISSbase] varchar(20)  NULL,
    [INSSbase] varchar(20)  NULL,
    [emailResponsavel] varchar(50)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [idUsuario] in table 'Usuarios'
ALTER TABLE [dbo].[Usuarios]
ADD CONSTRAINT [PK_Usuarios]
    PRIMARY KEY CLUSTERED ([idUsuario] ASC);
GO

-- Creating primary key on [idFormulario] in table 'Formulario'
ALTER TABLE [dbo].[Formulario]
ADD CONSTRAINT [PK_Formulario]
    PRIMARY KEY CLUSTERED ([idFormulario] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------