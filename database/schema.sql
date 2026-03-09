CREATE DATABASE finance_dashboard;
USE finance_dashboard;

CREATE TABLE usuarios (
    id INT AUTO_INCREMENT,
    nome VARCHAR(100) NOT NULL,
    sobrenome VARCHAR(150) NOT NULL,
    cpf CHAR(11) UNIQUE NOT NULL,
    email VARCHAR(255) UNIQUE NOT NULL,
    senha VARCHAR(255) NOT NULL,
    logradouro VARCHAR(255) NOT NULL,
    numero_do_logradouro VARCHAR(20) NOT NULL,
    cidade VARCHAR(150) NOT NULL,
    estado CHAR(2) NOT NULL,
    cep CHAR(8) NOT NULL,
    telefone_principal VARCHAR(20) NOT NULL,
    telefone_secundario VARCHAR(20) NOT NULL,
    ativo BOOLEAN NOT NULL DEFAULT TRUE,
    CONSTRAINT pk_usuarios
    PRIMARY KEY(id)
);

CREATE TABLE categorias (
    id INT AUTO_INCREMENT,
    id_usuario INT NOT NULL,
    nome VARCHAR(150) NOT NULL,
    ativo BOOLEAN NOT NULL DEFAULT TRUE,
    CONSTRAINT pk_categorias
    PRIMARY KEY(id),
    CONSTRAINT uk_categorias_usuarios
    UNIQUE (id_usuario, nome),
    CONSTRAINT fk_categorias_usuarios
    FOREIGN KEY (id_usuario) REFERENCES usuarios(id)
);

CREATE TABLE contas (
    id INT AUTO_INCREMENT,
    nome VARCHAR(100) NOT NULL,
    saldo DECIMAL(10, 2) NOT NULL DEFAULT 0.00,
    id_usuario INT NOT NULL,
    ativo BOOLEAN NOT NULL DEFAULT TRUE,
    CONSTRAINT pk_contas
    PRIMARY KEY(id),
    CONSTRAINT fk_contas_usuarios 
    FOREIGN KEY (id_usuario) REFERENCES usuarios(id),
    CONSTRAINT uk_contas_usuarios
    UNIQUE (id_usuario, nome)
);

CREATE TABLE lancamentos (
    id INT AUTO_INCREMENT,
    descricao VARCHAR(150),
    valor DECIMAL(10, 2) NOT NULL,
    data_de_lancamento DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    id_conta_origem INT NOT NULL,
    id_conta_destino INT,
    id_categoria INT NOT NULL,
    eh_entrada BOOLEAN NOT NULL,
    ativo BOOLEAN NOT NULL DEFAULT TRUE,
    CONSTRAINT pk_lancamentos
    PRIMARY KEY(id),
    CONSTRAINT fk_lancamentos_contas_origem 
    FOREIGN KEY (id_conta_origem) REFERENCES contas(id),
    CONSTRAINT fk_lancamentos_contas_destino
    FOREIGN KEY (id_conta_destino) REFERENCES contas(id),
    CONSTRAINT fk_lancamentos_categorias FOREIGN KEY (id_categoria) REFERENCES categorias(id),
    CONSTRAINT chk_lancamentos
    CHECK 
    (eh_entrada = TRUE AND id_conta_destino IS NULL)
    OR 
    (eh_entrada = FALSE AND (id_conta_destino IS NULL OR id_conta_destino <> id_conta_origem))
);

DELIMITER //

CREATE PROCEDURE inserirLancamento
(
arg_descricao VARCHAR(150),
arg_valor DECIMAL(10, 2),
arg_conta_origem INT,
arg_conta_destino INT,
arg_categoria INT,
arg_eh_entrada BOOLEAN
)
BEGIN
IF (SELECT ativo FROM categorias WHERE id = arg_categoria) = FALSE THEN SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Esta categoria está desativada!';
END IF;

IF (SELECT ativo FROM contas WHERE id = arg_conta_origem) = FALSE THEN SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Esta conta está desativada!';
END IF;

IF (arg_conta_destino IS NOT NULL) THEN
IF (SELECT id_usuario FROM contas WHERE id = arg_conta_origem) <> 
   (SELECT id_usuario FROM contas WHERE id = arg_conta_destino) THEN
    SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'As contas pertencem a usuários diferentes!';
END IF;

IF (SELECT ativo FROM contas WHERE id = arg_conta_destino) = FALSE THEN SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'A conta para a qual você está tentando transferir está desativada!';
END IF;

END IF;
START TRANSACTION;
INSERT INTO lancamentos(descricao, valor, id_conta_origem, id_conta_destino, id_categoria, eh_entrada)
VALUES 
(arg_descricao, arg_valor, arg_conta_origem, arg_conta_destino, arg_categoria, arg_eh_entrada);
IF (arg_conta_destino IS NOT NULL) THEN
UPDATE contas
SET saldo = saldo - arg_valor
WHERE id = arg_conta_origem;
UPDATE contas
SET saldo = saldo + arg_valor
WHERE id = arg_conta_destino;
ELSE 
IF (arg_eh_entrada = FALSE) THEN
UPDATE contas
SET saldo = saldo - arg_valor
WHERE id = arg_conta_origem;
ELSE
UPDATE contas
SET saldo = saldo + arg_valor
WHERE id = arg_conta_origem;
END IF;
END IF;
COMMIT;
END //

DELIMITER ;