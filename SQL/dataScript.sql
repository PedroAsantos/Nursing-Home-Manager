-- MEDICAMENTOS
insert into LarIdosos.MEDICINE(Name) values ('Benuron')
insert into LarIdosos.MEDICINE(Name) values ('Brufen')
insert into LarIdosos.MEDICINE(Name) values ('Hipobrufeno')
insert into LarIdosos.MEDICINE(Name) values ('Paracetamol')

-- BEDROOMS
insert into LarIdosos.BEDROOM (Capacity) values (2)
insert into LarIdosos.BEDROOM (Capacity) values (2)
insert into LarIdosos.BEDROOM (Capacity) values (2)

-- BEDS assumindo que ha 2 camas por quarto
insert into LarIdosos.BED (E_RoomNumber) values (1)
insert into LarIdosos.BED (E_RoomNumber) values (1)
insert into LarIdosos.BED (E_RoomNumber) values (2)
insert into LarIdosos.BED (E_RoomNumber) values (2)
insert into LarIdosos.BED (E_RoomNumber) values (3)
insert into LarIdosos.BED (E_RoomNumber) values (3)

-- DISEASE
insert into LarIdosos.DISEASE (Name) values ('Alzheimer')
insert into LarIdosos.DISEASE (Name) values ('Parkinson')
insert into LarIdosos.DISEASE (Name) values ('Hipertensao')
insert into LarIdosos.DISEASE (Name) values ('Diabetes')


-- DOCTOR
insert into LarIdosos.DOCTOR(NIF, Name, Phone, Address) values ('57432844Z', 'Joao Dinis', 964772218, 'Rua de Belem, Cristo, Aveiro')
insert into LarIdosos.DOCTOR(NIF, Name, Phone, Address) values ('57882373Z', 'Tiago Sousa', 915526612, 'Rua de Cacem, Moinho, Agueda')


-- VISITOR
insert into LarIdosos.VISITOR values ('128233732', 'George Dush', 'Rua Ali, Algures, Leiria', 935421212)
insert into LarIdosos.VISITOR values ('342337231', 'Alice Rodrigues', 'Rua Acolalem, Aqui, Aveiro', 965578128)

-- PATIENT
insert into LarIdosos.PATIENT values ('291119991', 'Maria Silva', 'f', 965566771, 76, '2016-2-21', null, 1, 1, '2016-2-21', null )
insert into LarIdosos.PATIENT values ('262167791', 'Mario Augusto', 'm', 919833381, 65, '2015-3-21', null, 0, 2, '2015-3-21', null )


-- DIAGNOSED
insert into LarIdosos.DIAGNOSED values ('Alzheimer', '291119991', 8, 0)
insert into LarIdosos.DIAGNOSED values ('Hipertensao', '291119991', 4, 0)
insert into LarIdosos.DIAGNOSED values ('Hipertensao', '262167791', 4, 0)
insert into LarIdosos.DIAGNOSED values ('Alzheimer', '262167791', 7, 0)


-- ACCOMPAINED

insert into LarIdosos.ACCOMPANIED values ('291119991','57432844Z')
insert into LarIdosos.ACCOMPANIED values ('262167791','57882373Z')


-- APPOINTMENT
insert into LarIdosos.APPOINTMENT values ('57882373Z','262167791', '2017-01-11', 'Psychiatry', 0)
insert into LarIdosos.APPOINTMENT values ('57882373Z','262167791', '2017-01-14', 'Psychiatry', 0)
insert into LarIdosos.APPOINTMENT values ('57882373Z','262167791', '2017-01-21', 'Psychiatry', 0)
insert into LarIdosos.APPOINTMENT values ('57432844Z','291119991', '2017-02-11', 'Psychiatry', 0)


-- MEDICINE
insert into LarIdosos.MEDICINE values ('Brufen')
insert into LarIdosos.MEDICINE values ('Paracetamol')
insert into LarIdosos.MEDICINE values ('Ben-u-ron')
insert into LarIdosos.MEDICINE values ('Anti-depressivo')

-- SCHEDULE
insert into LarIdosos.SCHEDULE values ('Monday', '17:40:00','262167791')
insert into LarIdosos.SCHEDULE values ('Thrusday', '18:40:00','262167791')
insert into LarIdosos.SCHEDULE values ('Thrusday', '18:40:00','291119991')


-- TAKING
insert into LarIdosos.TAKING values ('Monday', '17:40:00', '262167791', 1, 2, 0)
insert into LarIdosos.TAKING values ('Thrusday', '18:40:00', '262167791', 2, 2, 0)
insert into LarIdosos.TAKING values ('Thrusday', '18:40:00', '291119991', 1, 2, 0)
insert into LarIdosos.TAKING values ('Thrusday', '18:40:00', '291119991', 2, 2, 0)

-- FAMILY
insert into LarIdosos.FAMILY values ('128233732', 'Filho')
insert into LarIdosos.FAMILY values ('342337231', 'Filho')

-- DEPENDENT
insert into LarIdosos.DEPENDENT values ('128233732', '291119991')
insert into LarIdosos.DEPENDENT values ('342337231', '262167791')


-- VISIT
insert into LarIdosos.VISIT values ('2017-2-3')

-- RECEIVED
insert into LarIdosos.RECEIVED values ('291119991',1)

-- VISITED
insert into LarIdosos.VISITED values (1, '128233732')

--TYPE
insert into LarIdosos.Type values ('Cozinheiro',0)
insert into LarIdosos.Type values ('Auxiliar',0)
insert into LarIdosos.Type values ('Enfermeiro',0)
insert into LarIdosos.Type values ('Animador Social',0)

-- HUMAN RESOURCES
insert into LarIdosos.HumanResources values ('261617281', 'Quim Tozé', 963777881, 'Rua da Terça, Ribeira Brava', 480, '2016-9-28', 2)
insert into LarIdosos.HumanResources values ('266667999', 'Julio Nobrega', 985656881, 'Rua da Segunda, Ribeira Fria', 980, '2016-4-18', 1)
insert into LarIdosos.HumanResources values ('225375457', 'António Maia', 967562718, 'Aveiro, Rua da Saudade', 970, '2014-6-18', 1)

-- SHIFT
insert into LarIdosos.SHIFT values ('Monday', '14:00:00', '23:00:00')
insert into LarIdosos.SHIFT values ('Thursday', '14:00:00', '23:00:00')
insert into LarIdosos.SHIFT values ('Wednesday', '14:00:00', '23:00:00')

-- SHIFT INSTANCE
insert into LarIdosos.ShiftInstance values ('2016-9-28', null, '261617281',1)
insert into LarIdosos.ShiftInstance values ('2016-9-28', null, '261617281',2)
insert into LarIdosos.ShiftInstance values ('2016-9-28', null, '261617281',3)
insert into LarIdosos.ShiftInstance values ('2016-9-18', null, '266667999',1)
insert into LarIdosos.ShiftInstance values ('2016-10-8', null, '266667999',2)

-- FAULTS
insert into LarIdosos.Faults values ('2016-9-28', 1)

