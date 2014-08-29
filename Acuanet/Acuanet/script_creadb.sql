DROP DATABASE ntritondb;

CREATE DATABASE ntritondb;

use ntritondb;

-- Tabla de Participantes
CREATE TABLE participante(
	id SERIAL,
	nombre varchar(512),
	numero varchar(256),
	id_categoria int,	
	rama char(1),
	id_tag varchar(512),
	direccion text,
	pais varchar(512),
	prueba varchar(512),
	club varchar(512),
	email varchar(512),
	sexo char(1)
);

-- Tabla Catalogo de Categorias
CREATE TABLE categoria(
	id SERIAL,
	nombre varchar(512),	
	descripcion text
);

-- Tabla de resultados finales
create table resultado(
	id SERIAL,
	posicion int,
	id_participante int,
	tiempo varchar(256),
	fecha_hora_ini datetime(2),
	fecha_hora_fin datetime(2),
	milis_ini int,
	milis_fin int,
	tiempo_meta NUMERIC(23,10)
);


create table resultado_final(
	id SERIAL,
	posicion int,
	id_participante int,
	tiempo varchar(256),
	fecha_hora_ini datetime(2),
	fecha_hora_fin datetime(2),
	milis_ini int,
	milis_fin int,
	tiempo_meta NUMERIC(23,10)

);

-- Tabla definicion de Oleada
CREATE TABLE oleada(
	id SERIAL,
	id_categoria int,
	fecha_hora_ini_local datetime,
	fecha_hora_ini_antena datetime,
	milis_ini_local int,
	milis_ini_antena int
);


-- Tabla general de datos de tags leidos por el recptor en competencia
create table tags(
	id SERIAL,
	id_tag varchar(512),
	fecha_hora datetime,
	milis int,
	rssi double,
	lectura_v bool,
	marca int
);


