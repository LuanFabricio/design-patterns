pub struct Account {
    id: u32,
}

impl Account {
    pub fn new(id: u32) -> Self {
        Self { id }
    }

    pub fn get_id(&self) -> u32 {
        self.id
    }
}

pub enum EntryEnum {
    Credit,
    Debit,
}

impl EntryEnum {
    pub fn to_string(&self) -> String {
        match self {
            &EntryEnum::Credit => String::from("Credit"),
            &EntryEnum::Debit => String::from("Debit"),
        }
    }
}

pub struct Wallet {
    account: Account,
    current_money: i64,
}

impl Wallet {
    pub fn new(account: Account) -> Self {
        Self {
            account,
            current_money: 0,
        }
    }

    pub fn make_entry(&mut self, entry_type: EntryEnum, amount: u32) {
        println!(
            "Make entry to account {}: {} R${amount}",
            self.account.get_id(),
            entry_type.to_string()
        );

        match entry_type {
            EntryEnum::Credit => self.current_money += amount as i64,
            EntryEnum::Debit => self.current_money -= amount as i64,
        };

        println!("Current amount of money: R$ {}", self.current_money);
    }
}
