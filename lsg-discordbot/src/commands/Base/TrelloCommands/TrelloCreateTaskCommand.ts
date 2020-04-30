import { Command } from "discord-akairo";
import { Message } from "discord.js";
import { GuildMember } from "discord.js";
import { Repository } from "typeorm";
import { Users } from "../../../Models/UserDataModel";
import TrelloHelper from "../../../Helpers/TrelloHelper";

export default class TrelloCreateTaskCommand extends Command {
    constructor() {
        super('blad', {
            aliases: ['blad', 'bug', 'zadanie', 'task'],
            category: 'Trello',
            description: {
                content: 'Zglasza bląd do konkretnego użytkownika na trello',
                examples: ['blad @Algorytm jakis przykladowy blad'],
                usage: 'blad'
            },
            // cooldown: 120000,
            args: [
                {
                    id: 'member',
                    type: 'member',
                    prompt: {
                        start: (msg: Message) => `❔ ${msg.author}, uwzględnij osobę, której chcesz dodać zadanie do listy!`,
                        retry: (msg: Message) => `❔ ${msg.author} proszę... Uwzględnij prawidłową osobę, której chcesz dodać zadanie do listy!`
                    },
                },
                {
                    id: 'task',
                    type: 'string',
                    match: 'text',
                    default: 'Nie wprowadzono treści blędu'
                }
            ]
        });
    }

    public async exec(message: Message, { member, task }: { member: GuildMember, task: string }) {
        const userRepo: Repository<Users> = this.client.db.getRepository(Users);

        const developer = await userRepo.findOne({ userId: member.id });

        if (developer != undefined) {
            if (developer.trelloCardId == undefined) {
                return message.util.reply('Ta osoba nie ma zsynchronizowanej listy, przekaż zadanie innej');
            }

            await TrelloHelper.createCardForUserList(developer.trelloCardId, `${message.author.tag} (${message.author.id})`, task).then((res: any) => {

                message.util.reply(`✅ Utworzyłeś zadanie dla ${member.displayName}!`);

                // TODO: Na podstawie res.id zrobić nowy task i wlączyć w to użytkownika sendera i resolvera który rozwiązał zadanie (sender otrzymuje punkt za wykonane zadanie)
                console.log(`Utworzono card ${res.id}`);
            });
        } else {
            return message.util.reply('Ta osoba nie ma zsynchronizowanej listy, przekaż zadanie innej');
        }
    }

}