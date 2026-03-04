
--DESKTOP-JJ9DM3F\SQLEXPRESS
create database examenEFCrudMVC_API_NOSP
use examenEFCrudMVC_API_NOSP

create table Departamento
(
	idDepartamento int identity(1,1) not null,
	nombreDepartamento varchar(50) not null,
	constraint PK_Departamento primary key (idDepartamento)
)
insert into Departamento(nombreDepartamento)values('Marketing'),('Compras'),('Ventas'),('RH'),('TI')
select * from Departamento

create table Empleado
(
	idEmpleado int identity(1,1) not null,
	nombreEmpleado varchar(100) not null,
	idDepartamento int not null,
	activo bit default 1,
	constraint PK_Empleado primary key (idEmpleado),
	constraint FK_EmpleadoDepartamento foreign key (idDepartamento)
										references Departamento(idDepartamento)
)
insert into Empleado(nombreEmpleado,idDepartamento)values('Liz',4),('Adrian',5)
select * from Empleado