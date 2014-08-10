DROP DATABASE ntritondb;

CREATE DATABASE ntritondb;

use ntritondb;


CREATE TABLE participante(
id SERIAL,
nombre varchar(512),
numero varchar(256),
id_categoria int,	
rama char(1),
id_pic varchar(256),
direccion text,
club varchar(512)

);


CREATE TABLE categoria(
id SERIAL,
nombre varchar(512),	
descripcion text
);


create table resultado(
id SERIAL,
id_categoria int,
id_participante int,
tiempo varchar(256)

);


CREATE TABLE oleada(
id SERIAL,
nombre varchar(1024)
);





create table pics(

	id SERIAL,
	id_pic varchar(256),
	id_oleada int,
	fecha_hora date,
	milis int


);


