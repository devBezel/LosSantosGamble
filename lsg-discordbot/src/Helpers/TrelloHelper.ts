import { getTrelloBoardLists, createTrelloList, createTrelloCardForList } from '../Modules/TrelloModule';
import { trelloBoardId } from '../Config'
import { TrelloListModel } from '../Models/TrelloListModel';


export default class TrelloHelper {

    public static async isUserDiscordHaveList(userId: string): Promise<string> {
        let valueToReturn;

        await getTrelloBoardLists(trelloBoardId).then((lists: TrelloListModel[]) => {
            for(let list of lists) {
                console.log(`${list.name} [${userId === list.name}]`);
                if (userId === list.name) {
                    valueToReturn = list.id;
                    break;
                } else {
                    valueToReturn = undefined;
                    break;
                }
            }
        });
        console.log(`valueToReturn ${valueToReturn}`);
        return valueToReturn;
    }

    public static async createUserList(userId: string) {
        let response;
        await createTrelloList(userId, trelloBoardId).then((res: any) => {
            response = res;
        });

        return response;
    }

    public static async createCardForUserList(trelloListId: string, sender: string, task: string) {
        let response;
        await createTrelloCardForList(trelloListId, sender, task).then((res: any) => {
            response = res;
        });

        return response;
    }
}