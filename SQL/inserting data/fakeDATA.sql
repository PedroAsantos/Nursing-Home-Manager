-- MEDICAMENTOS
insert into exemplo1.MEDICINE(Name) values ('Benuron')
insert into exemplo1.MEDICINE(Name) values ('Brufen')
insert into exemplo1.MEDICINE(Name) values ('Hipobrufeno')
insert into exemplo1.MEDICINE(Name) values ('Paracetamol')

-- BEDROOMS
insert into exemplo1.BEDROOM (Capacity) values (2)
insert into exemplo1.BEDROOM (Capacity) values (2)
insert into exemplo1.BEDROOM (Capacity) values (2)
insert into exemplo1.BEDROOM (Capacity) values (2)
insert into exemplo1.BEDROOM (Capacity) values (2)
insert into exemplo1.BEDROOM (Capacity) values (2)
insert into exemplo1.BEDROOM (Capacity) values (2)
insert into exemplo1.BEDROOM (Capacity) values (2)

-- BEDS assumindo que ha 2 camas por quarto
insert into exemplo1.BED (E_RoomNumber) values (1)
insert into exemplo1.BED (E_RoomNumber) values (1)
insert into exemplo1.BED (E_RoomNumber) values (2)
insert into exemplo1.BED (E_RoomNumber) values (2)
insert into exemplo1.BED (E_RoomNumber) values (3)
insert into exemplo1.BED (E_RoomNumber) values (3)



-- DISEASE
insert into exemplo1.DISEASE (Name) values ('Alzheimer')
insert into exemplo1.DISEASE (Name) values ('Parkinson')
insert into exemplo1.DISEASE (Name) values ('Hipertensao')
insert into exemplo1.DISEASE (Name) values ('Diabetes')


-- DOCTOR
insert into exemplo1.DOCTOR(NIF, Name, Phone, Address) values ('57432844Z', 'Joao Dinis', 964772218, 'Rua de Belem, Cristo, Aveiro')
insert into exemplo1.DOCTOR(NIF, Name, Phone, Address) values ('57882373Z', 'Tiago Sousa', 915526612, 'Rua de Cacem, Moinho, Agueda')


-- VISITOR
insert into exemplo1.VISITOR values ('12823373Z', 'George Dush', 'Rua Ali, Algures, Leiria', 935421212)
insert into exemplo1.VISITOR values ('34233723Z', 'Alice Rodrigues', 'Rua Acolalem, Aqui, Aveiro', 965578128)