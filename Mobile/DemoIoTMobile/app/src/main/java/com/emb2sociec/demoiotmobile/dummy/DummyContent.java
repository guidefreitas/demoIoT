package com.emb2sociec.demoiotmobile.dummy;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

/**
 * Helper class for providing sample content for user interfaces created by
 * Android template wizards.
 * <p>
 * TODO: Replace all uses of this class before publishing your app.
 */
public class DummyContent {

    /**
     * An array of sample (dummy) items.
     */
    public static final List<DeviceItem> ITEMS = new ArrayList<DeviceItem>();

    /**
     * A map of sample (dummy) items, by ID.
     */
    public static final Map<String, DeviceItem> ITEM_MAP = new HashMap<String, DeviceItem>();

    private static final int COUNT = 25;

    static {
        // Add some sample items.
        for (int i = 1; i <= COUNT; i++) {
            addItem(createDummyItem(i));
        }
    }

    private static void addItem(DeviceItem item) {
        ITEMS.add(item);
        ITEM_MAP.put(item.id, item);
    }

    private static DeviceItem createDummyItem(int position) {
        return new DeviceItem(String.valueOf(position), "Item " + position, makeDetails(position));
    }

    private static String makeDetails(int position) {
        StringBuilder builder = new StringBuilder();
        builder.append("Details about Item: ").append(position);
        for (int i = 0; i < position; i++) {
            builder.append("\nMore details information here.");
        }
        return builder.toString();
    }

    /**
     * A dummy item representing a piece of content.
     */
    public static class DeviceItem {
        public final String id;
        public final String description;
        public final String details;

        public DeviceItem(String id, String description, String details) {
            this.id = id;
            this.description = description;
            this.details = details;
        }

        @Override
        public String toString() {
            return description;
        }
    }
}
