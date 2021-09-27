--CREATE DATABASE Atestados;

--Use Atestados;

--CREATE TABLE Persona (
--	PersonaID INT IDENTITY NOT NULL PRIMARY KEY,
--	Nombre VARCHAR(50) NOT NULL,
--	PrimerApellido VARCHAR(50) NOT NULL,
--	SegundoApellido VARCHAR(50),
--	Email VARCHAR(100) NOT NULL,
--	CategoriaActual INT,
--	TipoUsuario INT,
--	Telefono INT
--)

--CREATE TABLE Pais (
--	PaisID INT IDENTITY NOT NULL PRIMARY KEY,
--	Nombre VARCHAR(100)
--)

--CREATE TABLE TipoRubro (
--	TipoRubroID INT IDENTITY NOT NULL PRIMARY KEY,
--	Nombre VARCHAR(100)
--)

--CREATE TABLE Rubro (
--	RubroID INT IDENTITY NOT NULL PRIMARY KEY,
--	Nombre VARCHAR(100) NOT NULL,
--	Descripcion VARCHAR(1000),
--	TipoRubro INT NOT NULL,
--	CONSTRAINT Fk_TipoRubro FOREIGN KEY (TipoRubro) REFERENCES TipoRubro(TipoRubroID)
--)

--CREATE TABLE Atestado (
--	AtestadoID INT IDENTITY NOT NULL PRIMARY KEY,
--	Nombre VARCHAR(250) NOT NULL,
--	NumeroAutores INT NOT NULL,
--	Observaciones VARCHAR(1000),
--	HoraCreacion DATETIME NOT NULL,
--	Enviado INT,
--	Descargado INT,
--	CantidadHoras INT,
--	Lugar VARCHAR(250),
--	CatalogoTipo VARCHAR(100),
--	Enlace VARCHAR(250),
--	PaisID INT FOREIGN KEY REFERENCES Pais(PaisID),
--	PersonaID INT FOREIGN KEY REFERENCES Persona(PersonaID),
--	RubroID INT FOREIGN KEY REFERENCES Rubro(RubroID)
--)

--CREATE TABLE AtestadoXPersona (
--	AtestadoID INT FOREIGN KEY REFERENCES Atestado(AtestadoID),
--	PersonaID INT FOREIGN KEY REFERENCES Persona(PersonaID),
--	CONSTRAINT PK_AtestadoXPersona PRIMARY KEY (AtestadoID, PersonaID),
--	Porcentaje FLOAT,
--	Departamento VARCHAR(250)
--)

--CREATE TABLE Archivo (
--	ArchivoID INT NOT NULL PRIMARY KEY,
--	Obligatorio INT NOT NULL,
--	Nombre VARCHAR(100),
--	Datos VARBINARY(MAX),
--	TipoArchivo VARCHAR(200)
--)

--CREATE TABLE Fecha (
--	FechaID INT NOT NULL PRIMARY KEY,
--	FechaInicio DATE,
--	FechaFinal DATE
--)

--CREATE TABLE InfoEditorial (
--	InfoEditorialID INT NOT NULL PRIMARY KEY,
--	Editorial VARCHAR(100),
--	Website VARCHAR(250)
--)

--CREATE TABLE Idioma (
--	IdiomaID INT IDENTITY NOT NULL PRIMARY KEY,
--	Nombre VARCHAR(100)
--)

--CREATE TABLE DominioIdioma (
--	DominioIdiomaID INT IDENTITY NOT NULL PRIMARY KEY,
--	AtestadoID INT FOREIGN KEY REFERENCES Atestado(AtestadoID),
--	IdiomaID INT FOREIGN KEY REFERENCES Idioma(IdiomaID),
--	Lectura INT NOT NULL,
--	Escrito INT NOT NULL,
--	Auditiva INT NOT NULL,
--	Oral INT NOT NULL
--)



