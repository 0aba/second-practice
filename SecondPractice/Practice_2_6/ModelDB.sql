create table if not exists user_app
(
    pk SERIAL PRIMARY KEY,
    login VARCHAR(64) NOT NULL UNIQUE CHECK(LENGTH(login) >= 8),
    pass  VARCHAR(60) NOT NULL CHECK(LENGTH(login) >= 8) -- bf
);

create table if not exists todo_task
(
    pk SERIAL primary key,
    pk_user INT REFERENCES user_app(pk),
    title VARCHAR(64) NOT NULL UNIQUE,
    text_todo VARCHAR(128) NOT NULL,
    time_end TIMESTAMP NOT NULL,
    complited BOOL NOT NULL DEFAULT FALSE
);