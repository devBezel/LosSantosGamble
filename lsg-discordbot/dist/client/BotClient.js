"use strict";
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
const discord_akairo_1 = require("discord-akairo");
const path_1 = require("path");
const Config_1 = require("../Config");
const Database_1 = __importDefault(require("../Structures/Database"));
class BotClient extends discord_akairo_1.AkairoClient {
    constructor(config) {
        super({
            ownerID: config.owners,
        });
        this.listenerHandler = new discord_akairo_1.ListenerHandler(this, {
            directory: path_1.join(__dirname, '..', 'Listeners'),
        });
        this.commandHandler = new discord_akairo_1.CommandHandler(this, {
            directory: path_1.join(__dirname, "..", "Commands"),
            prefix: Config_1.prefix,
            ignorePermissions: Config_1.owners,
            allowMention: true,
            handleEdits: true,
            commandUtil: true,
            commandUtilLifetime: 3e5,
            defaultCooldown: 6e4,
            argumentDefaults: {
                prompt: {
                    modifyStart: (_, str) => `${str}\n\n Wpisz \`!cancel\` aby przerwać komende`,
                    modifyRetry: (_, str) => `${str}\n\n Wpisz \`!cancel\` aby przerwać komende`,
                    timeout: 'Czekam za długo, komenda została anulowana...',
                    ended: 'Przekroczyłeś maksimum prób, ta komenda została anulowana...',
                    retries: 3,
                    time: 3e4
                },
                otherwise: ""
            },
        });
        this.config = config;
    }
    async _init() {
        this.commandHandler.useListenerHandler(this.listenerHandler);
        this.listenerHandler.setEmitters({
            commandHandler: this.commandHandler,
            listenerHandler: this.listenerHandler,
            process
        });
        this.commandHandler.loadAll();
        this.listenerHandler.loadAll();
        this.db = Database_1.default.get(Config_1.databaseName);
        await this.db.connect();
        await this.db.synchronize();
    }
    async start() {
        await this._init();
        return this.login(this.config.token);
    }
}
exports.default = BotClient;
//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJmaWxlIjoiQm90Q2xpZW50LmpzIiwic291cmNlUm9vdCI6IiIsInNvdXJjZXMiOlsiLi4vLi4vc3JjL0NsaWVudC9Cb3RDbGllbnQudHMiXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6Ijs7Ozs7QUFBQSxtREFBK0U7QUFFL0UsK0JBQTRCO0FBQzVCLHNDQUF5RDtBQUV6RCxzRUFBNkM7QUFpQjdDLE1BQXFCLFNBQVUsU0FBUSw2QkFBWTtJQTZCL0MsWUFBbUIsTUFBa0I7UUFDakMsS0FBSyxDQUFDO1lBQ0YsT0FBTyxFQUFFLE1BQU0sQ0FBQyxNQUFNO1NBSXpCLENBQUMsQ0FBQztRQWhDQSxvQkFBZSxHQUFvQixJQUFJLGdDQUFlLENBQUMsSUFBSSxFQUFFO1lBQ2hFLFNBQVMsRUFBRSxXQUFJLENBQUMsU0FBUyxFQUFFLElBQUksRUFBRSxXQUFXLENBQUM7U0FDaEQsQ0FBQyxDQUFDO1FBRUksbUJBQWMsR0FBbUIsSUFBSSwrQkFBYyxDQUFDLElBQUksRUFBRTtZQUM3RCxTQUFTLEVBQUUsV0FBSSxDQUFDLFNBQVMsRUFBRSxJQUFJLEVBQUUsVUFBVSxDQUFDO1lBQzVDLE1BQU0sRUFBRSxlQUFNO1lBQ2QsaUJBQWlCLEVBQUUsZUFBTTtZQUN6QixZQUFZLEVBQUUsSUFBSTtZQUNsQixXQUFXLEVBQUUsSUFBSTtZQUNqQixXQUFXLEVBQUUsSUFBSTtZQUNqQixtQkFBbUIsRUFBRSxHQUFHO1lBQ3hCLGVBQWUsRUFBRSxHQUFHO1lBQ3BCLGdCQUFnQixFQUFFO2dCQUNkLE1BQU0sRUFBRTtvQkFDSixXQUFXLEVBQUUsQ0FBQyxDQUFDLEVBQUUsR0FBRyxFQUFVLEVBQUUsQ0FBQyxHQUFHLEdBQUcsNkNBQTZDO29CQUNwRixXQUFXLEVBQUUsQ0FBQyxDQUFDLEVBQUUsR0FBRyxFQUFVLEVBQUUsQ0FBQyxHQUFHLEdBQUcsNkNBQTZDO29CQUNwRixPQUFPLEVBQUUsK0NBQStDO29CQUN4RCxLQUFLLEVBQUUsOERBQThEO29CQUNyRSxPQUFPLEVBQUUsQ0FBQztvQkFDVixJQUFJLEVBQUUsR0FBRztpQkFDWjtnQkFDRCxTQUFTLEVBQUUsRUFBRTthQUNoQjtTQUNKLENBQUMsQ0FBQztRQVVDLElBQUksQ0FBQyxNQUFNLEdBQUcsTUFBTSxDQUFDO0lBQ3pCLENBQUM7SUFFTyxLQUFLLENBQUMsS0FBSztRQUNmLElBQUksQ0FBQyxjQUFjLENBQUMsa0JBQWtCLENBQUMsSUFBSSxDQUFDLGVBQWUsQ0FBQyxDQUFDO1FBQzdELElBQUksQ0FBQyxlQUFlLENBQUMsV0FBVyxDQUFDO1lBQzdCLGNBQWMsRUFBRSxJQUFJLENBQUMsY0FBYztZQUNuQyxlQUFlLEVBQUUsSUFBSSxDQUFDLGVBQWU7WUFDckMsT0FBTztTQUNWLENBQUMsQ0FBQztRQUVILElBQUksQ0FBQyxjQUFjLENBQUMsT0FBTyxFQUFFLENBQUM7UUFDOUIsSUFBSSxDQUFDLGVBQWUsQ0FBQyxPQUFPLEVBQUUsQ0FBQztRQUUvQixJQUFJLENBQUMsRUFBRSxHQUFHLGtCQUFRLENBQUMsR0FBRyxDQUFDLHFCQUFZLENBQUMsQ0FBQztRQUNyQyxNQUFNLElBQUksQ0FBQyxFQUFFLENBQUMsT0FBTyxFQUFFLENBQUM7UUFDeEIsTUFBTSxJQUFJLENBQUMsRUFBRSxDQUFDLFdBQVcsRUFBRSxDQUFDO0lBQ2hDLENBQUM7SUFFTSxLQUFLLENBQUMsS0FBSztRQUNkLE1BQU0sSUFBSSxDQUFDLEtBQUssRUFBRSxDQUFDO1FBRW5CLE9BQU8sSUFBSSxDQUFDLEtBQUssQ0FBQyxJQUFJLENBQUMsTUFBTSxDQUFDLEtBQUssQ0FBQyxDQUFDO0lBQ3pDLENBQUM7Q0FDSjtBQTdERCw0QkE2REMiLCJzb3VyY2VzQ29udGVudCI6WyJpbXBvcnQgeyBDb21tYW5kSGFuZGxlciwgTGlzdGVuZXJIYW5kbGVyLCBBa2Fpcm9DbGllbnQgfSBmcm9tICdkaXNjb3JkLWFrYWlybyc7XHJcbmltcG9ydCB7IE1lc3NhZ2UgfSBmcm9tICdkaXNjb3JkLmpzJztcclxuaW1wb3J0IHsgam9pbiB9IGZyb20gJ3BhdGgnO1xyXG5pbXBvcnQgeyBwcmVmaXgsIG93bmVycywgZGF0YWJhc2VOYW1lIH0gZnJvbSAnLi4vQ29uZmlnJzsgXHJcbmltcG9ydCB7IENvbm5lY3Rpb24gfSBmcm9tICd0eXBlb3JtJztcclxuaW1wb3J0IERhdGFiYXNlIGZyb20gJy4uL1N0cnVjdHVyZXMvRGF0YWJhc2UnXHJcblxyXG5kZWNsYXJlIG1vZHVsZSAnZGlzY29yZC1ha2Fpcm8nIHtcclxuICAgIGludGVyZmFjZSBBa2Fpcm9DbGllbnQge1xyXG4gICAgICAgIGNvbW1hbmRIYW5kbGVyOiBDb21tYW5kSGFuZGxlcjtcclxuICAgICAgICBsaXN0ZW5lckhhbmRsZXI6IExpc3RlbmVySGFuZGxlcjtcclxuICAgICAgICBjb25maWc6IEJvdE9wdGlvbnM7XHJcbiAgICAgICAgZGI6IENvbm5lY3Rpb247XHJcbiAgICB9XHJcbn1cclxuXHJcblxyXG5pbnRlcmZhY2UgQm90T3B0aW9ucyB7XHJcbiAgICB0b2tlbj86IHN0cmluZztcclxuICAgIG93bmVycz86IHN0cmluZyB8IHN0cmluZ1tdO1xyXG59XHJcblxyXG5leHBvcnQgZGVmYXVsdCBjbGFzcyBCb3RDbGllbnQgZXh0ZW5kcyBBa2Fpcm9DbGllbnQge1xyXG5cclxuICAgIHB1YmxpYyBkYiE6IENvbm5lY3Rpb247XHJcbiAgICBwdWJsaWMgbGlzdGVuZXJIYW5kbGVyOiBMaXN0ZW5lckhhbmRsZXIgPSBuZXcgTGlzdGVuZXJIYW5kbGVyKHRoaXMsIHtcclxuICAgICAgICBkaXJlY3Rvcnk6IGpvaW4oX19kaXJuYW1lLCAnLi4nLCAnTGlzdGVuZXJzJyksXHJcbiAgICB9KTtcclxuXHJcbiAgICBwdWJsaWMgY29tbWFuZEhhbmRsZXI6IENvbW1hbmRIYW5kbGVyID0gbmV3IENvbW1hbmRIYW5kbGVyKHRoaXMsIHtcclxuICAgICAgICBkaXJlY3Rvcnk6IGpvaW4oX19kaXJuYW1lLCBcIi4uXCIsIFwiQ29tbWFuZHNcIiksXHJcbiAgICAgICAgcHJlZml4OiBwcmVmaXgsXHJcbiAgICAgICAgaWdub3JlUGVybWlzc2lvbnM6IG93bmVycyxcclxuICAgICAgICBhbGxvd01lbnRpb246IHRydWUsXHJcbiAgICAgICAgaGFuZGxlRWRpdHM6IHRydWUsXHJcbiAgICAgICAgY29tbWFuZFV0aWw6IHRydWUsXHJcbiAgICAgICAgY29tbWFuZFV0aWxMaWZldGltZTogM2U1LFxyXG4gICAgICAgIGRlZmF1bHRDb29sZG93bjogNmU0LFxyXG4gICAgICAgIGFyZ3VtZW50RGVmYXVsdHM6IHtcclxuICAgICAgICAgICAgcHJvbXB0OiB7XHJcbiAgICAgICAgICAgICAgICBtb2RpZnlTdGFydDogKF8sIHN0cik6IHN0cmluZyA9PiBgJHtzdHJ9XFxuXFxuIFdwaXN6IFxcYCFjYW5jZWxcXGAgYWJ5IHByemVyd2HEhyBrb21lbmRlYCxcclxuICAgICAgICAgICAgICAgIG1vZGlmeVJldHJ5OiAoXywgc3RyKTogc3RyaW5nID0+IGAke3N0cn1cXG5cXG4gV3Bpc3ogXFxgIWNhbmNlbFxcYCBhYnkgcHJ6ZXJ3YcSHIGtvbWVuZGVgLFxyXG4gICAgICAgICAgICAgICAgdGltZW91dDogJ0N6ZWthbSB6YSBkxYJ1Z28sIGtvbWVuZGEgem9zdGHFgmEgYW51bG93YW5hLi4uJyxcclxuICAgICAgICAgICAgICAgIGVuZGVkOiAnUHJ6ZWtyb2N6ecWCZcWbIG1ha3NpbXVtIHByw7NiLCB0YSBrb21lbmRhIHpvc3RhxYJhIGFudWxvd2FuYS4uLicsXHJcbiAgICAgICAgICAgICAgICByZXRyaWVzOiAzLFxyXG4gICAgICAgICAgICAgICAgdGltZTogM2U0XHJcbiAgICAgICAgICAgIH0sXHJcbiAgICAgICAgICAgIG90aGVyd2lzZTogXCJcIlxyXG4gICAgICAgIH0sXHJcbiAgICB9KTtcclxuXHJcbiAgICBwdWJsaWMgY29uc3RydWN0b3IoY29uZmlnOiBCb3RPcHRpb25zKSB7XHJcbiAgICAgICAgc3VwZXIoe1xyXG4gICAgICAgICAgICBvd25lcklEOiBjb25maWcub3duZXJzLFxyXG4gICAgICAgICAgICAvLyBkaXNhYmxlZEV2ZW50czogWydUWVBJTkdfU1RBUlQnXSxcclxuICAgICAgICAgICAgLy8gc2hhcmRDb3VudDogMSxcclxuICAgICAgICAgICAgLy8gZGlzYWJsZUV2ZXJ5b25lOiB0cnVlXHJcbiAgICAgICAgfSk7XHJcblxyXG4gICAgICAgIHRoaXMuY29uZmlnID0gY29uZmlnO1xyXG4gICAgfVxyXG5cclxuICAgIHByaXZhdGUgYXN5bmMgX2luaXQoKTpQcm9taXNlPHZvaWQ+IHtcclxuICAgICAgICB0aGlzLmNvbW1hbmRIYW5kbGVyLnVzZUxpc3RlbmVySGFuZGxlcih0aGlzLmxpc3RlbmVySGFuZGxlcik7XHJcbiAgICAgICAgdGhpcy5saXN0ZW5lckhhbmRsZXIuc2V0RW1pdHRlcnMoe1xyXG4gICAgICAgICAgICBjb21tYW5kSGFuZGxlcjogdGhpcy5jb21tYW5kSGFuZGxlcixcclxuICAgICAgICAgICAgbGlzdGVuZXJIYW5kbGVyOiB0aGlzLmxpc3RlbmVySGFuZGxlcixcclxuICAgICAgICAgICAgcHJvY2Vzc1xyXG4gICAgICAgIH0pO1xyXG5cclxuICAgICAgICB0aGlzLmNvbW1hbmRIYW5kbGVyLmxvYWRBbGwoKTtcclxuICAgICAgICB0aGlzLmxpc3RlbmVySGFuZGxlci5sb2FkQWxsKCk7XHJcblxyXG4gICAgICAgIHRoaXMuZGIgPSBEYXRhYmFzZS5nZXQoZGF0YWJhc2VOYW1lKTtcclxuICAgICAgICBhd2FpdCB0aGlzLmRiLmNvbm5lY3QoKTtcclxuICAgICAgICBhd2FpdCB0aGlzLmRiLnN5bmNocm9uaXplKCk7XHJcbiAgICB9XHJcblxyXG4gICAgcHVibGljIGFzeW5jIHN0YXJ0KCk6IFByb21pc2U8c3RyaW5nPiB7XHJcbiAgICAgICAgYXdhaXQgdGhpcy5faW5pdCgpO1xyXG5cclxuICAgICAgICByZXR1cm4gdGhpcy5sb2dpbih0aGlzLmNvbmZpZy50b2tlbik7XHJcbiAgICB9XHJcbn1cclxuIl19