package com.emb2sociec.demoiotmobile;

import android.app.Application;
import android.content.Context;

/**
 * Created by guilherme on 04/09/17.
 */

public class DemoApplication extends Application {
    private static DemoApplication mInstance;
    private static Context mAppContext;

    @Override
    public void onCreate() {
        super.onCreate();
        mInstance = this;

        this.setAppContext(getApplicationContext());
    }

    public static DemoApplication getInstance(){
        return mInstance;
    }
    public static Context getAppContext() {
        return mAppContext;
    }
    public void setAppContext(Context mAppContext) {
        this.mAppContext = mAppContext;
    }
}
