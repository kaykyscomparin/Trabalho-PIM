create database PIMDB_desktop--Primeiro selecione e execute essa linha, ela cria o banco

use PIMDB_desktop --depois selecione e execute essa linha, ela coloca o banco em uso

--depois selecione e execute todos esses scripts para criaçao das tabelas usuario, endereco e produtos
--voce pode selecionar todas de uma vez e executar
create table usuario(
funcionario_id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
nome_usuario VARCHAR(150),
nome_completo VARCHAR(150)NOT NULL,
email_corporativo VARCHAR(150),
tel VARCHAR(15),
cpf VARCHAR(11)NOT NULL,
endereco_id INT NOT NULL,
senha VARCHAR(25) NOT NULL
FOREIGN KEY (endereco_id) REFERENCES endereco(endereco_id)
)

create table endereco(
endereco_id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
cep CHAR(9)NOT NULL,
cidade VARCHAR(35)NOT NULL,
bairro VARCHAR(35)NOT NULL,
rua VARCHAR(35)NOT NULL,
numero varchar(35)
)

CREATE TABLE Produtos (
produto_id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
nome_produto VARCHAR(60),
preco DECIMAL(10,2),
imgPrd VARBINARY(MAX),
estoque int
);

--essas linhas a baixo nao precisam ser executadas, elas servem apenas para visualizar
--as tabelas dentro do sql server
select * from usuario 
select * from Produtos
select * from endereco 
