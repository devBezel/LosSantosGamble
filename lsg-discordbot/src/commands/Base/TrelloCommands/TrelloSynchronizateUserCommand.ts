import { Command } from 'discord-akairo';
import { Message } from 'discord.js';
import { Repository } from 'typeorm';

import { Users } from '../../../Models/UserDataModel';
import TrelloHelper from '../../../Helpers/TrelloHelper';

export default class TrelloSynchronizateUserCommand extends Command {
    constructor() {
        super('synchronizuj', {
            aliases: ['synchronizuj'],
            category: 'Trello',
            description: {
                content: 'Synchronizuje listę gracza z discordem',
                examples: ['synchronizuj'],
                usage: 'synchronizuj'
            },
            cooldown: 120000,
            // ratelimit: 2,
        });
    }

    // TODO: Pobrawić ten kod jakoś, żeby synchronizate się nie powtarzał bo przeokropnie to wygląda
    public async exec(message: Message) {
        const discordUserId: string = message.author.id;
        const userRepo: Repository<Users> = this.client.db.getRepository(Users);

        const developer = await userRepo.findOne({ userId: discordUserId });
        
        if(developer === undefined) {

            await this.synchronizateTrello(message, discordUserId, userRepo, developer);

        } else {

            if(developer.trelloCardId !== undefined && developer.trelloCardId !== '') {
                return message.util.reply('❌ Twoje konto jest już zsynchronizowane z kartą na trello!');
            }

            await this.synchronizateTrello(message, discordUserId, userRepo, developer);
        }

        

        // if(developer == undefined) {
        //     await this.synchronizateTrello(message, discordUserId, userRepo, developer);
        // } else if(developer.trelloCardId !== undefined && developer.trelloCardId !== '') {
        //     return message.util.reply('❌ Twoje konto jest już zsynchronizowane z kartą na trello!');
        // } else {
        //     await this.synchronizateTrello(message, discordUserId, userRepo, developer);
        // }

    }

    public async synchronizateTrello(message: Message, discordUserId: string, userRepo: Repository<Users>, developer: Users) {

        await TrelloHelper.createUserList(discordUserId).then(async (res: any) => {
            await TrelloHelper.isUserDiscordHaveList(discordUserId).then(async (listId: string) => {

                if (listId == undefined) {
                    return message.util.reply('Upss... Coś poszło nie tak! Spróbuj ponownie później');
                }

                if(developer === undefined) {
                    await userRepo.insert({
                        userId: discordUserId,
                        trelloCardId: listId
                    });
                } else {
                    developer.trelloCardId = listId;

                    await userRepo.save(developer);
                }

                return message.util.reply('✅ Utworzono i zsynchronizowano listę z twoim discordem!');
            });
        });
    } 
}