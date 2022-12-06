using System;
using ZebraRfidSdk;

namespace ZebraRFIDApp.Model
{
    /// <summary>
    /// TagSeenCount class
    /// </summary>
    public class TagSeenCount
    {
        private string tagID;         private int seenCount;
        private TagData tagData;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:RfidDemo.VC.TagSeenCount"/> class.
        /// </summary>
        /// <param name="tagData">Tag data.</param>
        public TagSeenCount(TagData tagData)
        {
            TagData = tagData;
            TagID = tagData.Id;
            SeenCount = (tagData == null) ? 1 : (tagData.SeenCount == 0) ? 1 : tagData.SeenCount;
           
            

        }

        /// <summary>
        /// Gets or sets the tag Data.
        /// </summary>
        /// <value>The tag data.</value>
        public TagData TagData { get => tagData; set => tagData = value; }

        /// <summary>
        /// Gets or sets the tag identifier.
        /// </summary>
        /// <value>The tag identifier.</value>
        public string TagID { get => tagID; set => tagID = value; }

        /// <summary>
        /// Gets or sets the seen count.
        /// </summary>
        /// <value>The seen count.</value>
        public int SeenCount { get => seenCount; set => seenCount = value; }
    }
}