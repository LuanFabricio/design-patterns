use std::sync::{Mutex, OnceLock};

pub struct Database {
    db_id: u32,
}

impl Database {
    fn new(db_id: u32) -> Self {
        Self { db_id }
    }

    pub fn _hello(&self) {
        println!("Hello from db {}", self.db_id);
    }

    pub fn hello(instance: &'static Mutex<Database>) {
        instance.lock().unwrap()._hello();
    }
}

pub fn db_get_instance() -> &'static Mutex<Database> {
    static DATABASE: OnceLock<Mutex<Database>> = OnceLock::new();
    DATABASE.get_or_init(|| Mutex::new(Database::new(42)))
}
