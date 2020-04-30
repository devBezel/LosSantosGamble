import { Command } from 'discord-akairo';
import { Message } from 'discord.js';
import { listRequest, getTrelloBoardLists, createTrelloList } from '../../../Modules/TrelloModule';
import TrelloHelper from '../../../Helpers/TrelloHelper';
import { trelloBoardId } from '../../../Config'

export default class TrelloBoardCommand extends Command {
    constructor() {
        super('tablica', {
            aliases: ['tablica'],
            category: 'Trello',
            description: {
                content: 'Pokazuje tablice trello',
                examples: ['tablica'],
                usages: 'tablica'
            },
            ratelimit: 3
        });
    }

    //id listy 5df80be6b781864669ab1893
    public async exec(message: Message) {
        await createTrelloList('Testowa lista', trelloBoardId);
    }
}