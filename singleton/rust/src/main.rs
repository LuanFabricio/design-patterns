mod db;
use db::*;

fn main() {
    let db_connection1 = db_get_instance();
    let db_connection2 = db_get_instance();
    let db_connection3 = db_get_instance();

    Database::hello(db_connection1);
    Database::hello(db_connection2);
    Database::hello(db_connection3);
}
