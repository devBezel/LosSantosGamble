"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const discord_akairo_1 = require("discord-akairo");
const path_1 = require("path");
const Config_1 = require("../Config");
class BotClient extends discord_akairo_1.AkairoClient {
    constructor(config) {
        super({
            ownerID: config.owners,
        });
        this.listenerHandler = new discord_akairo_1.ListenerHandler(this, {
            directory: path_1.join(__dirname, '..', 'listeners'),
        });
        this.commandHandler = new discord_akairo_1.CommandHandler(this, {
            directory: path_1.join(__dirname, "..", "commands"),
            prefix: Config_1.prefix,
            ignorePermissions: Config_1.owners,
            allowMention: true,
            handleEdits: true,
            commandUtil: true,
            commandUtilLifetime: 3e5,
            defaultCooldown: 6e4,
            argumentDefaults: {
                prompt: {
                    modifyStart: (_, str) => `${str}\n\n Typ \`cancel\` aby przerwać komende`,
                    modifyRetry: (_, str) => `${str}\n\n Typ \`cancel\` aby przerwać komende`,
                    timeout: 'Czekasz za długo, komenda została anulowana...',
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
    }
    async start() {
        await this._init();
        return this.login(this.config.token);
    }
}
exports.default = BotClient;
//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJmaWxlIjoiQm90Q2xpZW50LmpzIiwic291cmNlUm9vdCI6IiIsInNvdXJjZXMiOlsiLi4vLi4vc3JjL2NsaWVudC9Cb3RDbGllbnQudHMiXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6Ijs7QUFBQSxtREFBK0U7QUFFL0UsK0JBQTRCO0FBQzVCLHNDQUEyQztBQWlCM0MsTUFBcUIsU0FBVSxTQUFRLDZCQUFZO0lBMkIvQyxZQUFtQixNQUFrQjtRQUNqQyxLQUFLLENBQUM7WUFDRixPQUFPLEVBQUUsTUFBTSxDQUFDLE1BQU07U0FJekIsQ0FBQyxDQUFDO1FBaENBLG9CQUFlLEdBQW9CLElBQUksZ0NBQWUsQ0FBQyxJQUFJLEVBQUU7WUFDaEUsU0FBUyxFQUFFLFdBQUksQ0FBQyxTQUFTLEVBQUUsSUFBSSxFQUFFLFdBQVcsQ0FBQztTQUNoRCxDQUFDLENBQUM7UUFFSSxtQkFBYyxHQUFtQixJQUFJLCtCQUFjLENBQUMsSUFBSSxFQUFFO1lBQzdELFNBQVMsRUFBRSxXQUFJLENBQUMsU0FBUyxFQUFFLElBQUksRUFBRSxVQUFVLENBQUM7WUFDNUMsTUFBTSxFQUFFLGVBQU07WUFDZCxpQkFBaUIsRUFBRSxlQUFNO1lBQ3pCLFlBQVksRUFBRSxJQUFJO1lBQ2xCLFdBQVcsRUFBRSxJQUFJO1lBQ2pCLFdBQVcsRUFBRSxJQUFJO1lBQ2pCLG1CQUFtQixFQUFFLEdBQUc7WUFDeEIsZUFBZSxFQUFFLEdBQUc7WUFDcEIsZ0JBQWdCLEVBQUU7Z0JBQ2QsTUFBTSxFQUFFO29CQUNKLFdBQVcsRUFBRSxDQUFDLENBQUMsRUFBRSxHQUFHLEVBQVUsRUFBRSxDQUFDLEdBQUcsR0FBRywwQ0FBMEM7b0JBQ2pGLFdBQVcsRUFBRSxDQUFDLENBQUMsRUFBRSxHQUFHLEVBQVUsRUFBRSxDQUFDLEdBQUcsR0FBRywwQ0FBMEM7b0JBQ2pGLE9BQU8sRUFBRSxnREFBZ0Q7b0JBQ3pELEtBQUssRUFBRSw4REFBOEQ7b0JBQ3JFLE9BQU8sRUFBRSxDQUFDO29CQUNWLElBQUksRUFBRSxHQUFHO2lCQUNaO2dCQUNELFNBQVMsRUFBRSxFQUFFO2FBQ2hCO1NBQ0osQ0FBQyxDQUFDO1FBVUMsSUFBSSxDQUFDLE1BQU0sR0FBRyxNQUFNLENBQUM7SUFDekIsQ0FBQztJQUVPLEtBQUssQ0FBQyxLQUFLO1FBQ2YsSUFBSSxDQUFDLGNBQWMsQ0FBQyxrQkFBa0IsQ0FBQyxJQUFJLENBQUMsZUFBZSxDQUFDLENBQUM7UUFDN0QsSUFBSSxDQUFDLGVBQWUsQ0FBQyxXQUFXLENBQUM7WUFDN0IsY0FBYyxFQUFFLElBQUksQ0FBQyxjQUFjO1lBQ25DLGVBQWUsRUFBRSxJQUFJLENBQUMsZUFBZTtZQUNyQyxPQUFPO1NBQ1YsQ0FBQyxDQUFDO1FBRUgsSUFBSSxDQUFDLGNBQWMsQ0FBQyxPQUFPLEVBQUUsQ0FBQztRQUM5QixJQUFJLENBQUMsZUFBZSxDQUFDLE9BQU8sRUFBRSxDQUFDO0lBQ25DLENBQUM7SUFFTSxLQUFLLENBQUMsS0FBSztRQUNkLE1BQU0sSUFBSSxDQUFDLEtBQUssRUFBRSxDQUFDO1FBRW5CLE9BQU8sSUFBSSxDQUFDLEtBQUssQ0FBQyxJQUFJLENBQUMsTUFBTSxDQUFDLEtBQUssQ0FBQyxDQUFDO0lBQ3pDLENBQUM7Q0FDSjtBQXZERCw0QkF1REMiLCJzb3VyY2VzQ29udGVudCI6WyJpbXBvcnQgeyBDb21tYW5kSGFuZGxlciwgTGlzdGVuZXJIYW5kbGVyLCBBa2Fpcm9DbGllbnQgfSBmcm9tICdkaXNjb3JkLWFrYWlybyc7XHJcbmltcG9ydCB7IE1lc3NhZ2UgfSBmcm9tICdkaXNjb3JkLmpzJztcclxuaW1wb3J0IHsgam9pbiB9IGZyb20gJ3BhdGgnO1xyXG5pbXBvcnQgeyBwcmVmaXgsIG93bmVycyB9IGZyb20gJy4uL0NvbmZpZyc7IFxyXG5cclxuXHJcbmRlY2xhcmUgbW9kdWxlICdkaXNjb3JkLWFrYWlybycge1xyXG4gICAgaW50ZXJmYWNlIEFrYWlyb0NsaWVudCB7XHJcbiAgICAgICAgY29tbWFuZEhhbmRsZXI6IENvbW1hbmRIYW5kbGVyO1xyXG4gICAgICAgIGxpc3RlbmVySGFuZGxlcjogTGlzdGVuZXJIYW5kbGVyO1xyXG4gICAgICAgIGNvbmZpZzogQm90T3B0aW9ucztcclxuICAgIH1cclxufVxyXG5cclxuXHJcbmludGVyZmFjZSBCb3RPcHRpb25zIHtcclxuICAgIHRva2VuPzogc3RyaW5nO1xyXG4gICAgb3duZXJzPzogc3RyaW5nIHwgc3RyaW5nW107XHJcbn1cclxuXHJcbmV4cG9ydCBkZWZhdWx0IGNsYXNzIEJvdENsaWVudCBleHRlbmRzIEFrYWlyb0NsaWVudCB7XHJcbiAgICBwdWJsaWMgbGlzdGVuZXJIYW5kbGVyOiBMaXN0ZW5lckhhbmRsZXIgPSBuZXcgTGlzdGVuZXJIYW5kbGVyKHRoaXMsIHtcclxuICAgICAgICBkaXJlY3Rvcnk6IGpvaW4oX19kaXJuYW1lLCAnLi4nLCAnbGlzdGVuZXJzJyksXHJcbiAgICB9KTtcclxuXHJcbiAgICBwdWJsaWMgY29tbWFuZEhhbmRsZXI6IENvbW1hbmRIYW5kbGVyID0gbmV3IENvbW1hbmRIYW5kbGVyKHRoaXMsIHtcclxuICAgICAgICBkaXJlY3Rvcnk6IGpvaW4oX19kaXJuYW1lLCBcIi4uXCIsIFwiY29tbWFuZHNcIiksXHJcbiAgICAgICAgcHJlZml4OiBwcmVmaXgsXHJcbiAgICAgICAgaWdub3JlUGVybWlzc2lvbnM6IG93bmVycyxcclxuICAgICAgICBhbGxvd01lbnRpb246IHRydWUsXHJcbiAgICAgICAgaGFuZGxlRWRpdHM6IHRydWUsXHJcbiAgICAgICAgY29tbWFuZFV0aWw6IHRydWUsXHJcbiAgICAgICAgY29tbWFuZFV0aWxMaWZldGltZTogM2U1LFxyXG4gICAgICAgIGRlZmF1bHRDb29sZG93bjogNmU0LFxyXG4gICAgICAgIGFyZ3VtZW50RGVmYXVsdHM6IHtcclxuICAgICAgICAgICAgcHJvbXB0OiB7XHJcbiAgICAgICAgICAgICAgICBtb2RpZnlTdGFydDogKF8sIHN0cik6IHN0cmluZyA9PiBgJHtzdHJ9XFxuXFxuIFR5cCBcXGBjYW5jZWxcXGAgYWJ5IHByemVyd2HEhyBrb21lbmRlYCxcclxuICAgICAgICAgICAgICAgIG1vZGlmeVJldHJ5OiAoXywgc3RyKTogc3RyaW5nID0+IGAke3N0cn1cXG5cXG4gVHlwIFxcYGNhbmNlbFxcYCBhYnkgcHJ6ZXJ3YcSHIGtvbWVuZGVgLFxyXG4gICAgICAgICAgICAgICAgdGltZW91dDogJ0N6ZWthc3ogemEgZMWCdWdvLCBrb21lbmRhIHpvc3RhxYJhIGFudWxvd2FuYS4uLicsXHJcbiAgICAgICAgICAgICAgICBlbmRlZDogJ1ByemVrcm9jennFgmXFmyBtYWtzaW11bSBwcsOzYiwgdGEga29tZW5kYSB6b3N0YcWCYSBhbnVsb3dhbmEuLi4nLFxyXG4gICAgICAgICAgICAgICAgcmV0cmllczogMyxcclxuICAgICAgICAgICAgICAgIHRpbWU6IDNlNFxyXG4gICAgICAgICAgICB9LFxyXG4gICAgICAgICAgICBvdGhlcndpc2U6IFwiXCJcclxuICAgICAgICB9LFxyXG4gICAgfSk7XHJcblxyXG4gICAgcHVibGljIGNvbnN0cnVjdG9yKGNvbmZpZzogQm90T3B0aW9ucykge1xyXG4gICAgICAgIHN1cGVyKHtcclxuICAgICAgICAgICAgb3duZXJJRDogY29uZmlnLm93bmVycyxcclxuICAgICAgICAgICAgLy8gZGlzYWJsZWRFdmVudHM6IFsnVFlQSU5HX1NUQVJUJ10sXHJcbiAgICAgICAgICAgIC8vIHNoYXJkQ291bnQ6IDEsXHJcbiAgICAgICAgICAgIC8vIGRpc2FibGVFdmVyeW9uZTogdHJ1ZVxyXG4gICAgICAgIH0pO1xyXG5cclxuICAgICAgICB0aGlzLmNvbmZpZyA9IGNvbmZpZztcclxuICAgIH1cclxuXHJcbiAgICBwcml2YXRlIGFzeW5jIF9pbml0KCk6UHJvbWlzZTx2b2lkPiB7XHJcbiAgICAgICAgdGhpcy5jb21tYW5kSGFuZGxlci51c2VMaXN0ZW5lckhhbmRsZXIodGhpcy5saXN0ZW5lckhhbmRsZXIpO1xyXG4gICAgICAgIHRoaXMubGlzdGVuZXJIYW5kbGVyLnNldEVtaXR0ZXJzKHtcclxuICAgICAgICAgICAgY29tbWFuZEhhbmRsZXI6IHRoaXMuY29tbWFuZEhhbmRsZXIsXHJcbiAgICAgICAgICAgIGxpc3RlbmVySGFuZGxlcjogdGhpcy5saXN0ZW5lckhhbmRsZXIsXHJcbiAgICAgICAgICAgIHByb2Nlc3NcclxuICAgICAgICB9KTtcclxuXHJcbiAgICAgICAgdGhpcy5jb21tYW5kSGFuZGxlci5sb2FkQWxsKCk7XHJcbiAgICAgICAgdGhpcy5saXN0ZW5lckhhbmRsZXIubG9hZEFsbCgpO1xyXG4gICAgfVxyXG5cclxuICAgIHB1YmxpYyBhc3luYyBzdGFydCgpOiBQcm9taXNlPHN0cmluZz4ge1xyXG4gICAgICAgIGF3YWl0IHRoaXMuX2luaXQoKTtcclxuXHJcbiAgICAgICAgcmV0dXJuIHRoaXMubG9naW4odGhpcy5jb25maWcudG9rZW4pO1xyXG4gICAgfVxyXG59XHJcbiJdfQ==