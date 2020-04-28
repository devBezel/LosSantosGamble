import { Command } from 'discord-akairo';
import { Message } from 'discord.js';

export default class UpCommand extends Command {
    public constructor() {
        super('up', {
            aliases: ['up'],
            category: 'Base',
            description: {
                content: 'Sprawdza czy bot jest aktywny',
                examples: ['up'],
                usages: 'up'
            },
            ratelimit: 3
        });
    }


    public exec(message: Message) {
        return message.util.reply(`up został wykonany w: ${this.client.ws.ping}ms`);
    }
}