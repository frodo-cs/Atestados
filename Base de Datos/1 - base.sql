CREATE DATABASE Atestados;

Use Atestados;

CREATE TABLE Persona (
	PersonaID INT IDENTITY NOT NULL PRIMARY KEY,
	Nombre VARCHAR(50) NOT NULL,
	PrimerApellido VARCHAR(50) NOT NULL,
	SegundoApellido VARCHAR(50),
	Email VARCHAR(100) NOT NULL,
	CategoriaActual INT,
	TipoUsuario INT,
	Telefono INT
)

CREATE TABLE Usuario (
	UsuarioID INT NOT NULL PRIMARY KEY,
	Email VARCHAR(100) NOT NULL ,
	Contrasena VARCHAR(200) NOT NULL,
	FOREIGN KEY (UsuarioID) REFERENCES Persona(PersonaID)
)

CREATE TABLE Pais (
	PaisID INT IDENTITY NOT NULL PRIMARY KEY,
	Nombre VARCHAR(100) NOT NULL
)

CREATE TABLE TipoRubro (
	TipoRubroID INT IDENTITY NOT NULL PRIMARY KEY,
	Nombre VARCHAR(100) NOT NULL
)

CREATE TABLE Rubro (
	RubroID INT IDENTITY NOT NULL PRIMARY KEY,
	Nombre VARCHAR(100) NOT NULL,
	Descripcion VARCHAR(1000),
	TipoRubro INT NOT NULL,
	CONSTRAINT Fk_TipoRubro FOREIGN KEY (TipoRubro) REFERENCES TipoRubro(TipoRubroID)
)

CREATE TABLE Atestado (
	AtestadoID INT IDENTITY NOT NULL PRIMARY KEY,
	Nombre VARCHAR(250) NOT NULL,
	NumeroAutores INT NOT NULL,
	Observaciones VARCHAR(1000),
	Codigo VARCHAR(100),
	HoraCreacion DATETIME NOT NULL,
	Enviado INT,
	Descargado INT,
	CantidadHoras INT,
	Lugar VARCHAR(250),
	CatalogoTipo VARCHAR(100),
	Enlace VARCHAR(250),
	PaisID INT NOT NULL FOREIGN KEY REFERENCES Pais(PaisID),
	PersonaID INT NOT NULL FOREIGN KEY REFERENCES Persona(PersonaID),
	RubroID INT NOT NULL FOREIGN KEY REFERENCES Rubro(RubroID)
)

CREATE TABLE AtestadoXPersona (
	AtestadoID INT NOT NULL,
	PersonaID INT NOT NULL,
	CONSTRAINT PK_AtestadoXPersona PRIMARY KEY (AtestadoID, PersonaID),
	Porcentaje FLOAT,
	Departamento VARCHAR(250),
	CONSTRAINT FK_AtestadoXPersona_AtestadoID FOREIGN KEY (AtestadoID) REFERENCES Atestado(AtestadoID) ON DELETE CASCADE,
	CONSTRAINT FK_AtestadoXPersona_PersonaID FOREIGN KEY (PersonaID) REFERENCES Persona(PersonaID) ON DELETE CASCADE
)

CREATE TABLE Archivo (
	ArchivoID INT IDENTITY NOT NULL PRIMARY KEY,
	Obligatorio INT NOT NULL,
	Nombre VARCHAR(100) NOT NULL,
	Datos VARBINARY(MAX) NOT NULL,
	TipoArchivo NVARCHAR(200) NOT NULL,
	AtestadoID INT NOT NULL,
	CONSTRAINT FK_Archivo_AtestadoID FOREIGN KEY (AtestadoID) REFERENCES Atestado(AtestadoID) ON DELETE CASCADE
)

CREATE TABLE Fecha (
	FechaID INT NOT NULL PRIMARY KEY,
	FechaInicio DATE,
	FechaFinal DATE,
	CONSTRAINT FK_Fecha_AtestadoID FOREIGN KEY (FechaID) REFERENCES Atestado(AtestadoID) ON DELETE CASCADE
)

CREATE TABLE InfoEditorial (
	InfoEditorialID INT NOT NULL PRIMARY KEY,
	Editorial VARCHAR(100) NOT NULL,
	Website VARCHAR(250) NOT NULL,
	CONSTRAINT FK_InfoEditorial_AtestadoID FOREIGN KEY (InfoEditorialID) REFERENCES Atestado(AtestadoID) ON DELETE CASCADE
)

CREATE TABLE Idioma (
	IdiomaID INT IDENTITY NOT NULL PRIMARY KEY,
	Nombre VARCHAR(100) NOT NULL
)

CREATE TABLE DominioIdioma (
	DominioIdiomaID INT NOT NULL PRIMARY KEY,
	IdiomaID INT FOREIGN KEY REFERENCES Idioma(IdiomaID),
	Lectura INT NOT NULL,
	Escrito INT NOT NULL,
	Auditiva INT NOT NULL,
	Oral INT NOT NULL,
	CONSTRAINT FK_DominioIdioma_AtestadoID FOREIGN KEY (DominioIdiomaID) REFERENCES Atestado(AtestadoID) ON DELETE CASCADE
)