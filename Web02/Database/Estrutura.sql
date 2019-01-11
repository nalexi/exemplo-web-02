CREATE TABLE categorias (
	id int PRIMARY KEY IDENTITY(1,1),
	nome VARCHAR(100),
	registro_ativo bit

);

CREATE TABLE pessoas(
	id INT PRIMARY KEY IDENTITY(1,1),
	nome VARCHAR(100),
	cpf VARCHAR(14),
	rg VARCHAR (19),
	data_nascimento DATETIME,
	sexo CHAR,
	idade SMALLINT,
	registro_ativo bit,

);

CREATE TABLE enderecos(
	id INT PRIMARY KEY IDENTITY(1,1),
	estado VARCHAR(2),
	cidade VARCHAR(100),
	bairro VARCHAR(60),
	cep VARCHAR(10),
	numero INT,
	complemento TEXT,
	registro_ativo bit,	

);

SELECT *FROM pessoas;