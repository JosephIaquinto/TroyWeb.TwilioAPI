using System;
using System.Activities;
using System.Threading;
using System.Threading.Tasks;
using TroyWeb.TwilioAPI.Activities.Properties;
using UiPath.Shared.Activities;
using UiPath.Shared.Activities.Localization;
using UiPath.Shared.Activities.Utilities;

namespace TroyWeb.TwilioAPI.Activities
{
    [LocalizedDisplayName(nameof(Resources.GetMessagingPricing_DisplayName))]
    [LocalizedDescription(nameof(Resources.GetMessagingPricing_Description))]
    public class GetMessagingPricing : ContinuableAsyncCodeActivity
    {
        #region Properties

        /// <summary>
        /// If set, continue executing the remaining activities even if the current activity has failed.
        /// </summary>
        [LocalizedCategory(nameof(Resources.Common_Category))]
        [LocalizedDisplayName(nameof(Resources.ContinueOnError_DisplayName))]
        [LocalizedDescription(nameof(Resources.ContinueOnError_Description))]
        public override InArgument<bool> ContinueOnError { get; set; }

        [LocalizedDisplayName(nameof(Resources.GetMessagingPricing_CountryCode_DisplayName))]
        [LocalizedDescription(nameof(Resources.GetMessagingPricing_CountryCode_Description))]
        [LocalizedCategory(nameof(Resources.Input_Category))]
        public InArgument<string> CountryCode { get; set; }

        [LocalizedDisplayName(nameof(Resources.GetMessagingPricing_CountryResource_DisplayName))]
        [LocalizedDescription(nameof(Resources.GetMessagingPricing_CountryResource_Description))]
        [LocalizedCategory(nameof(Resources.Output_Category))]
        public OutArgument<object> CountryResource { get; set; }

        #endregion


        #region Constructors

        public GetMessagingPricing()
        {
            Constraints.Add(ActivityConstraints.HasParentType<GetMessagingPricing, TwilioApiScope>(string.Format(Resources.ValidationScope_Error, Resources.TwilioApiScope_DisplayName)));
        }

        #endregion


        #region Protected Methods

        protected override void CacheMetadata(CodeActivityMetadata metadata)
        {

            base.CacheMetadata(metadata);
        }

        protected override async Task<Action<AsyncCodeActivityContext>> ExecuteAsync(AsyncCodeActivityContext context, CancellationToken cancellationToken)
        {
            // Object Container: Use objectContainer.Get<T>() to retrieve objects from the scope
            var objectContainer = context.GetFromContext<IObjectContainer>(TwilioApiScope.ParentContainerPropertyTag);

            // Inputs
            var countrycode = CountryCode.Get(context);
    
            ///////////////////////////
            // Add execution logic HERE
            ///////////////////////////

            // Outputs
            return (ctx) => {
                CountryResource.Set(ctx, null);
            };
        }

        #endregion
    }
}

