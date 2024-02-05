use crate::wallet::WalletFacade;

mod dependency;
mod wallet;

fn main() {
    let mut wallet = WalletFacade::new(42);

    wallet.add_money_to_wallet(4200);

    wallet.deduct_money_from_wallet(42);
}
