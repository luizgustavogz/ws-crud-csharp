-- CREATE DATABASE testeLuiz
USE testeLuiz

-- Vídeo para criar usuário: https://www.youtube.com/watch?v=zIBsTOtdwS0&ab_channel=ThiagodaSilvaAdriano
-- user: usrluiz		senha: 1234
-- TRUNCATE TABLE tblClientes
-- SELECT * FROM vwClientes
-- SELECT * FROM tblClientes
-- SELECT * FROM tblNacionalidades


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


-- 5. Inserir nacionalidades
INSERT INTO tblNacionalidades (Nacionalidade) VALUES ('Brasileira');
INSERT INTO tblNacionalidades (Nacionalidade) VALUES ('Italiana');
INSERT INTO tblNacionalidades (Nacionalidade) VALUES ('Argentina');
INSERT INTO tblNacionalidades (Nacionalidade) VALUES ('Espanhola');
INSERT INTO tblNacionalidades (Nacionalidade) VALUES ('Outras');


-- 6. Inserir clientes
--INSERT INTO tblClientes (Nome, CPF, Telefone, Sexo, Nacionalidade_id, DataNascimento, DataHoraCadastro)
--VALUES ('Luiz Gustavo', 12345678910, 11945266591, 'M', 4, GETDATE(), GETDATE())
