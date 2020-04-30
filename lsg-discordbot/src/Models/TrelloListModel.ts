export interface TrelloListModel {
    id: string;
    name: string;
    closed: boolean;
    pos: number;
    softLimit: any;
    idBoard: string;
    subscribed: boolean;
}