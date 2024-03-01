-- CREATE DATABASE testeLuiz
USE testeLuiz

-- usrluiz		1234

-- 1. Criar a tabela tblClientes
CREATE TABLE tblClientes (
    ID int PRIMARY KEY IDENTITY,
    Nome VARCHAR(200),
    CPF VARCHAR(20),
    Telefone VARCHAR(20),
    Sexo VARCHAR(1),
    Nacionalidade_id SMALLINT,
    DataNascimento DATE,
    DataHoraCadastro DATETIME
);

-- 2. Criar a tabela tblNacionalidades
CREATE TABLE tblNacionalidades (
    Id SMALLINT PRIMARY KEY IDENTITY,
    Nacionalidade VARCHAR(100)
);

-- 3. Adicionar a restrição de chave estrangeira na tblClientes
ALTER TABLE tblClientes
ADD CONSTRAINT FK_tblClientes_Nacionalidades FOREIGN KEY (Nacionalidade_id)
REFERENCES tblNacionalidades(Id);

-- 4. Criar a view vwClientes
CREATE VIEW vwClientes AS
SELECT c.ID, c.Nome, c.CPF, c.Telefone, c.Sexo, n.Nacionalidade, c.DataNascimento, c.DataHoraCadastro
FROM tblClientes c
LEFT JOIN tblNacionalidades n ON c.Nacionalidade_id = n.Id;
-- SELECT * FROM vwClientes

-- 5. Inserir nacionalidades
INSERT INTO tblNacionalidades (Nacionalidade) VALUES ('Brasileira');
INSERT INTO tblNacionalidades (Nacionalidade) VALUES ('Italiana');
INSERT INTO tblNacionalidades (Nacionalidade) VALUES ('Argentina');
INSERT INTO tblNacionalidades (Nacionalidade) VALUES ('Espanhola');
INSERT INTO tblNacionalidades (Nacionalidade) VALUES ('Outras');

