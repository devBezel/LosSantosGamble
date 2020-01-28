import { CommandHandler, ListenerHandler, AkairoClient } from 'discord-akairo';
import { Message } from 'discord.js';
import { join } from 'path';
import { prefix, owners } from '../Config'; 


declare module 'discord-akairo' {
    interface AkairoClient {
        commandHandler: CommandHandler;
        listenerHandler: ListenerHandler;
        config: BotOptions;
    }
}


interface BotOptions {
    token?: string;
    owners?: string | string[];
}

export default class BotClient extends AkairoClient {
    public commandHandler: CommandHandler = new CommandHandler(this, {
        directory: join(__dirname, "..", "commands"),
        prefix: prefix,
        ignorePermissions: owners,
        handleEdits: true,
        commandUtil: true,
        commandUtilLifetime: 3e5,
        defaultCooldown: 1e4,
        argumentDefaults: {
            prompt: {
                modifyStart: (_, str): string => `${str}\n\n Typ \`cancel\` aby przerwać komende`,
                modifyRetry: (_, str): string => `${str}\n\n Typ \`cancel\` aby przerwać komende`,
                timeout: 'Czekasz za długo, komenda została anulowana...',
                ended: 'Przekroczyłeś maksimum prób, ta komenda została anulowana...',
                retries: 3,
                time: 3e4
            },
            otherwise: ""
        }
    });

    public listerHandler: ListenerHandler = new ListenerHandler(this, {
        directory: join(__dirname, '..', 'listeners'),
    });

    public constructor(config: BotOptions) {
        super({
            ownerID: owners,
            disabledEvents: ['TYPING_START'],
            shardCount: 1,
            disableEveryone: true
        });

        this.config = config;
    }

    private async _init():Promise<void> {
        this.commandHandler.useListenerHandler(this.listenerHandler);
        this.listenerHandler.setEmitters({
            commandHandler: this.commandHandler,
            listenerHandler: this.listenerHandler,
            proccess: process
        });

        this.commandHandler.loadAll();
        this.listenerHandler.loadAll();
    }

    public async start(): Promise<string> {
        await this._init();

        return this.login(this.config.token);
    }
}
