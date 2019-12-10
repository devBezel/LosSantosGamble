export interface Character {
    id: number;
    account: Account;
    name: string;
    surname: string;
    gender: string;
    height: number;
    weight: number;
    description: string;
    history: string;
    picUrl: null;
    posX: number;
    posY: number;
    posZ: number;
    rotation: number;
    money: number;
    dirtyMoney: number;
    bank: number;
    bankStatus: boolean;
    health: number;
    armor: number;
}
