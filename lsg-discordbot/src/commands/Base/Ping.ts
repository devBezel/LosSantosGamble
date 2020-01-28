import { Command } from 'discord-akairo';
import { Message } from 'discord.js';

export default class PingCommand extends Command {
    public constructor() {
        super('ping', {
            aliases: ['ping'],
            category: 'Base',
            description: {
                content: 'Sprawdza czy bot jest aktywny',
                examples: ['ping'],
                usages: 'ping'
            },
            ratelimit: 3
        });
    }


    public exec(message: Message) {
        return message.util.reply(`Ping został odpity w: ${this.client.ws.ping}ms`);
    }
}