trait Prototype {
    fn from_self(self: &Self) -> Self;
    fn clone(&self) -> Self;
}

#[derive(Debug)]
struct Enemy {
    hp: u32,
    attack: u32,
    defense: u32,
    xp: u32,
}

impl Enemy {
    fn new() -> Self {
        Self {
            hp: 0,
            attack: 0,
            defense: 0,
            xp: 0,
        }
    }

    pub fn add_attack(&mut self, bonus_attack: u32) {
        self.attack += bonus_attack;
    }

    pub fn add_hp(&mut self, bonus_hp: u32) {
        self.hp += bonus_hp;
    }

    pub fn add_defense(&mut self, bonus_defense: u32) {
        self.defense += bonus_defense;
    }

    pub fn add_xp(&mut self, bonus_xp: u32) {
        self.xp += bonus_xp;
    }
}

impl Prototype for Enemy {
    fn from_self(self: &Self) -> Self {
        let mut clone = Self::new();
        clone.hp = self.hp;
        clone.attack = self.attack;
        clone.defense = self.defense;
        clone.xp = self.xp;

        clone
    }

    fn clone(&self) -> Self {
        Self::from_self(self)
    }
}

fn main() {
    let mut e1 = Enemy::new();
    e1.add_hp(42);
    e1.add_attack(42);
    e1.add_defense(42);
    e1.add_xp(42);

    let mut e2 = e1.clone();
    e2.add_attack(31);

    println!("Enemy 1: {:?}", e1);
    println!("Enemy 2: {:?}", e2);
}
