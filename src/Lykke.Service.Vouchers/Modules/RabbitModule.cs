﻿using Autofac;
using JetBrains.Annotations;
using Lykke.Job.QuorumTransactionWatcher.Contract;
using Lykke.RabbitMqBroker.Publisher;
using Lykke.RabbitMqBroker.Subscriber;
using Lykke.Service.Vouchers.Contract;
using Lykke.Service.Vouchers.Rabbit.Subscribers;
using Lykke.Service.Vouchers.Settings;
using Lykke.Service.Vouchers.Settings.Service.Rabbit;
using Lykke.SettingsReader;

namespace Lykke.Service.Vouchers.Modules
{
    [UsedImplicitly]
    public class RabbitModule : Module
    {
        private const string TokensReservedExchange = "lykke.wallet.vouchertokensreserved";
        private const string TokensUsedExchange = "lykke.wallet.vouchertokensused";
        private const string QueueSuffix = "vouchers";

        private readonly RabbitSettings _settings;

        public RabbitModule(IReloadingManager<AppSettings> settings)
        {
            _settings = settings.CurrentValue.VouchersService.Rabbit;
        }

        protected override void Load(ContainerBuilder builder)
        {
            RegisterPublishers(builder);

            RegisterSubscribers(builder);
        }

        private void RegisterPublishers(ContainerBuilder builder)
        {
            builder.RegisterJsonRabbitPublisher<VoucherTokensReservedEvent>(
                _settings.Publishers.VoucherTokensReservedConnString, TokensReservedExchange);

            builder.RegisterJsonRabbitPublisher<VoucherTokensUsedEvent>(
                _settings.Publishers.VoucherTokensReservedConnString, TokensUsedExchange);
        }

        private void RegisterSubscribers(ContainerBuilder builder)
        {
            var undecodedEventExchange = Context.GetEndpointName<UndecodedEvent>();
            builder.RegisterJsonRabbitSubscriber<UndecodedEventSubscriber, UndecodedEvent>(
                _settings.Subscribers.UndecodedEventConnString,
                undecodedEventExchange,
                QueueSuffix);
        }
    }
}