use crate::dependency::{Account, EntryEnum, Wallet};

pub struct WalletFacade {
    wallet: Wallet,
}

impl WalletFacade {
    pub fn new(account_id: u32) -> Self {
        Self {
            wallet: Wallet::new(Account::new(account_id)),
        }
    }

    pub fn add_money_to_wallet(&mut self, amount: u32) {
        self.wallet.make_entry(EntryEnum::Credit, amount);
    }

    pub fn deduct_money_from_wallet(&mut self, amount: u32) {
        self.wallet.make_entry(EntryEnum::Debit, amount);
    }
}
