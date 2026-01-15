# Optimizely Opal Setup Guide

## Prerequisites
1. An Optimizely account (sign up at https://www.optimizely.com/)
2. Access to Optimizely Opal (Feature Experimentation)

## Step 1: Get Your SDK Key

1. Log in to your Optimizely dashboard
2. Navigate to **Settings** → **Environments**
3. Copy your **SDK Key** for the environment you want to use (Development, Production, etc.)

## Step 2: Configure Your Application

### For Render.com Deployment:

1. Go to your Render.com dashboard
2. Select your `useless-facts-tool` web service
3. Go to **Environment** tab
4. Add these environment variables:
   ```
   Optimizely__SdkKey = your-actual-sdk-key-here
   Optimizely__Enabled = true
   ```

### For Local Development:

Update `appsettings.json`:
```json
{
  "Optimizely": {
    "SdkKey": "your-actual-sdk-key-here",
    "Enabled": true
  }
}
```

## Step 3: Create Feature Flags in Optimizely

1. In your Optimizely dashboard, go to **Flags**
2. Click **Create New Flag**
3. Create a flag named: `facts_feature`
4. Add variables if needed:
   - `variation` (string): "control", "treatment", "default"
5. Set up your experiment rules and variations
6. Save and enable the flag

## Step 4: Test Your Integration

### Test the API endpoints:

```bash
# With feature flag evaluation
curl https://useless-facts-tool.onrender.com/api/facts/random

# Without feature flags
curl https://useless-facts-tool.onrender.com/api/facts/random/bypass
```

The response will include a `featureVariation` field showing which variation was served.

## Example Response

```json
{
  "success": true,
  "data": {
    "text": "Honey never spoils. Archaeologists have found 3000-year-old honey in Egyptian tombs that was still edible.",
    "permalink": "https://uselessfacts.jsph.pl/...",
    "source": "djtech.net"
  },
  "message": "Successfully fetched a useless fact!",
  "featureVariation": "treatment"
}
```

## Advanced: A/B Testing Scenarios

You can use Optimizely to:
- **Control who sees facts**: Enable/disable the feature for specific user segments
- **Test variations**: Show different facts sources or formats to different users
- **Gradual rollouts**: Roll out to 10%, 50%, 100% of users
- **Targeting**: Target by user attributes, location, etc.

## Troubleshooting

If you see `featureVariation: "default"`:
- Check that your SDK key is correct
- Verify the feature flag exists and is enabled in Optimizely
- Check the application logs for Optimizely initialization errors

If the feature is always disabled:
- Ensure `Optimizely__Enabled` is set to `true`
- Verify your SDK key is valid and not "YOUR_SDK_KEY_HERE"
