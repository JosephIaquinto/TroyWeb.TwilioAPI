using System;
using System.Activities;
using System.Threading;
using System.Threading.Tasks;
using TroyWeb.TwilioAPI.Activities.Properties;
using TroyWeb.TwilioAPI.Wrappers.Fax;
using Twilio.Clients;
using UiPath.Shared.Activities;
using UiPath.Shared.Activities.Localization;
using UiPath.Shared.Activities.Utilities;

namespace TroyWeb.TwilioAPI.Activities
{
    [LocalizedDisplayName(nameof(Resources.DeleteFaxMedia_DisplayName))]
    [LocalizedDescription(nameof(Resources.DeleteFaxMedia_Description))]
    public class DeleteFaxMedia : ContinuableAsyncCodeActivity
    {
        #region Properties

        /// <summary>
        /// If set, continue executing the remaining activities even if the current activity has failed.
        /// </summary>
        [LocalizedCategory(nameof(Resources.Common_Category))]
        [LocalizedDisplayName(nameof(Resources.ContinueOnError_DisplayName))]
        [LocalizedDescription(nameof(Resources.ContinueOnError_Description))]
        public override InArgument<bool> ContinueOnError { get; set; }

        [LocalizedDisplayName(nameof(Resources.DeleteFaxMedia_FaxSid_DisplayName))]
        [LocalizedDescription(nameof(Resources.DeleteFaxMedia_FaxSid_Description))]
        [LocalizedCategory(nameof(Resources.Input_Category))]
        public InArgument<string> FaxSid { get; set; }

        [LocalizedDisplayName(nameof(Resources.DeleteFaxMedia_FaxMediaSid_DisplayName))]
        [LocalizedDescription(nameof(Resources.DeleteFaxMedia_FaxMediaSid_Description))]
        [LocalizedCategory(nameof(Resources.Input_Category))]
        public InArgument<string> FaxMediaSid { get; set; }

        [LocalizedDisplayName(nameof(Resources.DeleteFaxMedia_Success_DisplayName))]
        [LocalizedDescription(nameof(Resources.DeleteFaxMedia_Success_Description))]
        [LocalizedCategory(nameof(Resources.Output_Category))]
        public OutArgument<bool> Success { get; set; }

        #endregion


        #region Constructors

        public DeleteFaxMedia()
        {
            Constraints.Add(ActivityConstraints.HasParentType<DeleteFaxMedia, TwilioApiScope>(string.Format(Resources.ValidationScope_Error, Resources.TwilioApiScope_DisplayName)));
        }

        #endregion


        #region Protected Methods

        protected override void CacheMetadata(CodeActivityMetadata metadata)
        {
            metadata.AddRequiredArgument(FaxSid, nameof(FaxSid));
            metadata.AddRequiredArgument(FaxMediaSid, nameof(FaxMediaSid));

            base.CacheMetadata(metadata);
        }

        protected override async Task<Action<AsyncCodeActivityContext>> ExecuteAsync(AsyncCodeActivityContext context, CancellationToken cancellationToken)
        {
            // Object Container: Use objectContainer.Get<T>() to retrieve objects from the scope
            var objectContainer = context.GetFromContext<IObjectContainer>(TwilioApiScope.ParentContainerPropertyTag);

            // Inputs
            var faxsid = FaxSid.Get(context);
            var faxmediasid = FaxMediaSid.Get(context);

            var deleted =
                await FaxMediaWrappers.DeleteFaxMediaAsync(objectContainer.Get<ITwilioRestClient>(), faxsid,
                    faxmediasid);

            // Outputs
            return (ctx) => {
                Success.Set(ctx, deleted);
            };
        }

        #endregion
    }
}

