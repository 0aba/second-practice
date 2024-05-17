create table IF NOT EXISTS user_app
(
    pk SERIAL PRIMARY KEY,
    login VARCHAR(64) NOT NULL UNIQUE CHECK(LENGTH(login) >= 8),
    pass  VARCHAR(60) NOT NULL CHECK(LENGTH(pass) >= 8) -- bf
);

create table IF NOT EXISTS todo_task
(
    pk SERIAL PRIMARY KEY,
    pk_user INT REFERENCES user_app(pk),
    title VARCHAR(64) NOT NULL,
    text_todo VARCHAR(128) NOT NULL,
    time_end TIMESTAMP NOT NULL,
    complited BOOL NOT NULL DEFAULT FALSE
);