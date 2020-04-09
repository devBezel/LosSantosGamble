"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const discord_akairo_1 = require("discord-akairo");
class PingCommand extends discord_akairo_1.Command {
    constructor() {
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
    exec(message) {
        return message.util.reply(`Ping został odpity w: ${this.client.ws.ping}ms`);
    }
}
exports.default = PingCommand;
//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJmaWxlIjoiUGluZy5qcyIsInNvdXJjZVJvb3QiOiIiLCJzb3VyY2VzIjpbIi4uLy4uLy4uL3NyYy9jb21tYW5kcy9CYXNlL1BpbmcudHMiXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6Ijs7QUFBQSxtREFBeUM7QUFHekMsTUFBcUIsV0FBWSxTQUFRLHdCQUFPO0lBQzVDO1FBQ0ksS0FBSyxDQUFDLE1BQU0sRUFBRTtZQUNWLE9BQU8sRUFBRSxDQUFDLE1BQU0sQ0FBQztZQUNqQixRQUFRLEVBQUUsTUFBTTtZQUNoQixXQUFXLEVBQUU7Z0JBQ1QsT0FBTyxFQUFFLCtCQUErQjtnQkFDeEMsUUFBUSxFQUFFLENBQUMsTUFBTSxDQUFDO2dCQUNsQixNQUFNLEVBQUUsTUFBTTthQUNqQjtZQUNELFNBQVMsRUFBRSxDQUFDO1NBQ2YsQ0FBQyxDQUFDO0lBQ1AsQ0FBQztJQUdNLElBQUksQ0FBQyxPQUFnQjtRQUN4QixPQUFPLE9BQU8sQ0FBQyxJQUFJLENBQUMsS0FBSyxDQUFDLHlCQUF5QixJQUFJLENBQUMsTUFBTSxDQUFDLEVBQUUsQ0FBQyxJQUFJLElBQUksQ0FBQyxDQUFDO0lBQ2hGLENBQUM7Q0FDSjtBQWxCRCw4QkFrQkMiLCJzb3VyY2VzQ29udGVudCI6WyJpbXBvcnQgeyBDb21tYW5kIH0gZnJvbSAnZGlzY29yZC1ha2Fpcm8nO1xyXG5pbXBvcnQgeyBNZXNzYWdlIH0gZnJvbSAnZGlzY29yZC5qcyc7XHJcblxyXG5leHBvcnQgZGVmYXVsdCBjbGFzcyBQaW5nQ29tbWFuZCBleHRlbmRzIENvbW1hbmQge1xyXG4gICAgcHVibGljIGNvbnN0cnVjdG9yKCkge1xyXG4gICAgICAgIHN1cGVyKCdwaW5nJywge1xyXG4gICAgICAgICAgICBhbGlhc2VzOiBbJ3BpbmcnXSxcclxuICAgICAgICAgICAgY2F0ZWdvcnk6ICdCYXNlJyxcclxuICAgICAgICAgICAgZGVzY3JpcHRpb246IHtcclxuICAgICAgICAgICAgICAgIGNvbnRlbnQ6ICdTcHJhd2R6YSBjenkgYm90IGplc3QgYWt0eXdueScsXHJcbiAgICAgICAgICAgICAgICBleGFtcGxlczogWydwaW5nJ10sXHJcbiAgICAgICAgICAgICAgICB1c2FnZXM6ICdwaW5nJ1xyXG4gICAgICAgICAgICB9LFxyXG4gICAgICAgICAgICByYXRlbGltaXQ6IDNcclxuICAgICAgICB9KTtcclxuICAgIH1cclxuXHJcblxyXG4gICAgcHVibGljIGV4ZWMobWVzc2FnZTogTWVzc2FnZSkge1xyXG4gICAgICAgIHJldHVybiBtZXNzYWdlLnV0aWwucmVwbHkoYFBpbmcgem9zdGHFgiBvZHBpdHkgdzogJHt0aGlzLmNsaWVudC53cy5waW5nfW1zYCk7XHJcbiAgICB9XHJcbn0iXX0=