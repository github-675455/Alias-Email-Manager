CREATE TABLE aliases
(
    id bigserial primary key,
    alias text,
    email text,
    enabled boolean,
    CONSTRAINT uniq_alias UNIQUE (alias)
);