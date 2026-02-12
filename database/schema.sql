CREATE DATABASE finance_dashboard;
USE finance_dashboard;

CREATE TABLE usuarios (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nome VARCHAR(100) NOT NULL,
    sobrenome VARCHAR(100) NOT NULL,
    email VARCHAR(150) UNIQUE NOT NULL,
    senha VARCHAR(255) NOT NULL,
    logradouro VARCHAR(255),
    numero_do_logradouro VARCHAR(20),
    cidade VARCHAR(100),
    estado VARCHAR(2),
    cep VARCHAR(10),
    telefone_principal VARCHAR(20),
    telefone_secundario VARCHAR(20)
);

CREATE TABLE categorias (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nome VARCHAR(100) UNIQUE NOT NULL
);

CREATE TABLE contas (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nome VARCHAR(100) UNIQUE NOT NULL,
    saldo DECIMAL(10, 2) NOT NULL DEFAULT 0.00,
    id_usuario INT NOT NULL,
    CONSTRAINT fk_contas_usuarios FOREIGN KEY (id_usuario) REFERENCES usuarios(id)
);

CREATE TABLE lancamentos (
    id INT AUTO_INCREMENT PRIMARY KEY,
    descricao VARCHAR(255),
    valor DECIMAL(10, 2) NOT NULL,
    data_de_lancamento TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    id_conta INT NOT NULL,
    id_categoria INT,
    CONSTRAINT fk_lancamentos_contas FOREIGN KEY (id_conta) REFERENCES contas(id),
    CONSTRAINT fk_lancamentos_categorias FOREIGN KEY (id_categoria) REFERENCES categorias(id)
);

CREATE TABLE entradas (
    id INT PRIMARY KEY,
    CONSTRAINT fk_entradas_lancamentos FOREIGN KEY (id) REFERENCES lancamentos(id) ON DELETE CASCADE
);

CREATE TABLE saidas (
    id INT PRIMARY KEY,
    CONSTRAINT fk_saidas_lancamentos FOREIGN KEY (id) REFERENCES lancamentos(id) ON DELETE CASCADE
);

CREATE TABLE transferencias (
    id INT PRIMARY KEY,
    id_conta_destino INT NOT NULL,
    CONSTRAINT fk_transferencias_saidas FOREIGN KEY (id) REFERENCES saidas(id) ON DELETE CASCADE,
    CONSTRAINT fk_transferencias_destino FOREIGN KEY (id_conta_destino) REFERENCES contas(id)
);