using System;
using System.Activities;
using System.Threading;
using System.Threading.Tasks;
using TroyWeb.TwilioAPI.Activities.Properties;
using TroyWeb.TwilioAPI.Wrappers.Fax;
using Twilio.Base;
using Twilio.Clients;
using Twilio.Rest.Fax.V1;
using UiPath.Shared.Activities;
using UiPath.Shared.Activities.Localization;
using UiPath.Shared.Activities.Utilities;

namespace TroyWeb.TwilioAPI.Activities
{
    [LocalizedDisplayName(nameof(Resources.GetFaxPage_DisplayName))]
    [LocalizedDescription(nameof(Resources.GetFaxPage_Description))]
    public class GetFaxPage : ContinuableAsyncCodeActivity
    {
        #region Properties

        /// <summary>
        /// If set, continue executing the remaining activities even if the current activity has failed.
        /// </summary>
        [LocalizedCategory(nameof(Resources.Common_Category))]
        [LocalizedDisplayName(nameof(Resources.ContinueOnError_DisplayName))]
        [LocalizedDescription(nameof(Resources.ContinueOnError_Description))]
        public override InArgument<bool> ContinueOnError { get; set; }

        [LocalizedDisplayName(nameof(Resources.GetFaxPage_TargetUrl_DisplayName))]
        [LocalizedDescription(nameof(Resources.GetFaxPage_TargetUrl_Description))]
        [LocalizedCategory(nameof(Resources.Input_Category))]
        public InArgument<string> TargetUrl { get; set; }

        [LocalizedDisplayName(nameof(Resources.GetFaxPage_FaxPage_DisplayName))]
        [LocalizedDescription(nameof(Resources.GetFaxPage_FaxPage_Description))]
        [LocalizedCategory(nameof(Resources.Output_Category))]
        public OutArgument<Page<FaxResource>> FaxPage { get; set; }

        #endregion


        #region Constructors

        public GetFaxPage()
        {
            Constraints.Add(ActivityConstraints.HasParentType<GetFaxPage, TwilioApiScope>(string.Format(Resources.ValidationScope_Error, Resources.TwilioApiScope_DisplayName)));
        }

        #endregion


        #region Protected Methods

        protected override void CacheMetadata(CodeActivityMetadata metadata)
        {
            if (TargetUrl == null) metadata.AddValidationError(string.Format(Resources.ValidationValue_Error, nameof(TargetUrl)));

            base.CacheMetadata(metadata);
        }

        protected override async Task<Action<AsyncCodeActivityContext>> ExecuteAsync(AsyncCodeActivityContext context, CancellationToken cancellationToken)
        {
            // Object Container: Use objectContainer.Get<T>() to retrieve objects from the scope
            var objectContainer = context.GetFromContext<IObjectContainer>(TwilioApiScope.ParentContainerPropertyTag);

            // Inputs
            var targeturl = TargetUrl.Get(context);

            var page = FaxWrappers.GetFaxPage(objectContainer.Get<ITwilioRestClient>(), targeturl);

            // Outputs
            return (ctx) => {
                FaxPage.Set(ctx, page);
            };
        }

        #endregion
    }
}
