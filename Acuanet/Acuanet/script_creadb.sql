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
id_categoria int,
id_participante int,
tiempo varchar(256)

);

-- Tabla definicion de Oleada posee el nombre del evento y el momento de inicio
CREATE TABLE oleada(
id SERIAL,
nombre varchar(1024),
fecha_hora_ini datetime
);


-- Tabla general de datos
create table tags(
	id SERIAL,
	id_tag varchar(512),
	id_oleada int,
	fecha_hora datetime,
	milis int,
	lectura_v bool
);






