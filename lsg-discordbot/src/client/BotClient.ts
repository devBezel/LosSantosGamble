import { CommandHandler, ListenerHandler, AkairoClient } from 'discord-akairo';
import { Message } from 'discord.js';
import { join } from 'path';
import { prefix, owners, databaseName } from '../Config'; 
import { Connection } from 'typeorm';
import Database from '../Structures/Database'

declare module 'discord-akairo' {
    interface AkairoClient {
        commandHandler: CommandHandler;
        listenerHandler: ListenerHandler;
        config: BotOptions;
        db: Connection;
    }
}


interface BotOptions {
    token?: string;
    owners?: string | string[];
}

export default class BotClient extends AkairoClient {

    public db!: Connection;
    public listenerHandler: ListenerHandler = new ListenerHandler(this, {
        directory: join(__dirname, '..', 'Listeners'),
    });

    public commandHandler: CommandHandler = new CommandHandler(this, {
        directory: join(__dirname, "..", "Commands"),
        prefix: prefix,
        ignorePermissions: owners,
        allowMention: true,
        handleEdits: true,
        commandUtil: true,
        commandUtilLifetime: 3e5,
        defaultCooldown: 6e4,
        argumentDefaults: {
            prompt: {
                modifyStart: (_, str): string => `${str}\n\n Wpisz \`cancel\` aby przerwać komende`,
                modifyRetry: (_, str): string => `${str}\n\n Wpisz \`cancel\` aby przerwać komende`,
                timeout: 'Czekam za długo, komenda została anulowana...',
                ended: 'Przekroczyłeś maksimum prób, ta komenda została anulowana...',
                retries: 3,
                time: 3e4
            },
            otherwise: ""
        },
    });

    public constructor(config: BotOptions) {
        super({
            ownerID: config.owners,
            // disabledEvents: ['TYPING_START'],
            // shardCount: 1,
            // disableEveryone: true
        });

        this.config = config;
    }

    private async _init():Promise<void> {
        this.commandHandler.useListenerHandler(this.listenerHandler);
        this.listenerHandler.setEmitters({
            commandHandler: this.commandHandler,
            listenerHandler: this.listenerHandler,
            process
        });

        this.commandHandler.loadAll();
        this.listenerHandler.loadAll();

        this.db = Database.get(databaseName);
        await this.db.connect();
        await this.db.synchronize();
    }

    public async start(): Promise<string> {
        await this._init();

        return this.login(this.config.token);
    }
}
