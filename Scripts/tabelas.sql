CREATE TABLE Escolaridade (
Id integer IDENTITY(1,1) primary key,
Descricao Varchar(50)
);

CREATE TABLE Usuarios (
Id integer IDENTITY(1,1) primary key,
Nome Varchar(50),
Sobrenome Varchar(150),
Email Varchar(80),
DataNascimento Date,
EscolaridadeId integer FOREIGN KEY REFERENCES Escolaridade(Id)
);
