CREATE TABLE Heroi
(
    Id INT PRIMARY KEY,
    Nome NVARCHAR(120) NOT NULL,
    NomeHeroi NVARCHAR(120) NOT NULL,
    DataNascimento DATETIME NOT NULL,
    Altura FLOAT NOT NULL,
    Peso FLOAT NOT NULL,
    SuperPoder NVARCHAR(120) NOT NULL
);