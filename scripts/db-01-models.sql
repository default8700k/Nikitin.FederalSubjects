--

create schema if not exists models;

--

create table if not exists models.federal_districts (
    id smallserial primary key,
    name varchar(128) not null,

    unique (system_name) -- also unique index
);

create table if not exists models.federal_subject_types (
    id smallserial primary key,
    name varchar(128) not null,

    unique (name) -- also unique index
);

create table if not exists models.federal_subjects (
    id smallserial primary key,
    federal_district_id smallint not null,
    federal_subject_type_id smallint not null,
    name varchar(128) not null,
    description text,
    content text,

    unique (name), -- also unique index

    foreign key (federal_district_id) references models.federal_districts (id),
    foreign key (federal_subject_type_id) references models.federal_subject_types (id)
);

create table if not exists models.map (
    id smallserial primary key,
    federal_subject_id smallint not null,
    path text,

    foreign key (federal_subject_id) references models.federal_subjects (id)
);

--
