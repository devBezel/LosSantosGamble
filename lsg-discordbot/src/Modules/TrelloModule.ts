import TrelloNodeAPI from '../../node_modules/trello-node-api';
import  { trelloOAuthToken, trelloApiKey }  from '../Config';
import { TrelloListModel } from '../Models/TrelloListModel';

const trello = new TrelloNodeAPI();





export const listRequest = async (listId: string) => {
    trello.setApiKey(trelloApiKey);
    trello.setOauthToken(trelloOAuthToken);


    let res;
    await trello.list.search(listId).then((response: TrelloListModel) => {
        console.log(response);
        res = response;
    }, error => {
        console.log(error);
    });

    return res;
}

export const getTrelloBoardLists = async (boardId: string) => {
    trello.setApiKey(trelloApiKey);
    trello.setOauthToken(trelloOAuthToken);

    let res;
    await trello.board.searchLists(boardId).then((response: TrelloListModel[]) => {
        res = response;
    }, error => {
        console.log(error);
    })

    return res;
};

export const createTrelloList = async (name: string, boardId: string) => {
    trello.setApiKey(trelloApiKey);
    trello.setOauthToken(trelloOAuthToken);

    const data: any = {
        name: name,
        idBoard: boardId,
    };

    let reponse;
    await trello.list.create(data).then((response: any) => {
        console.log(reponse);
    }, error => {
        console.log(error);
    });
};

export const createTrelloCardForList = async (listId: string, content: string, description: string) => {
    trello.setApiKey(trelloApiKey);
    trello.setOauthToken(trelloOAuthToken);

    const data: any = {
        name: content,
        desc: description,
        idList: listId
    };

    let response;
    await trello.card.create(data).then((res: any) => {
        response = res;
    }, error => {
        console.log(error);
    });

    return response;
};

// export const getCardsFromListRequest = async (listId: string) => {
//     trello.setApiKey('0d62ed37fd938f7c0663cdb46dac5dbe');
//     trello.setOauthToken('39131d2a0e9e7a93848a11f0d355d023e644f6f045db79857d91722ae9609453');

//     let res;
//     await trello.list.(listId)
// }